// Play JAMT launcher: serves the mockups folder on a local port and opens the browser.
// Build: C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /nologo /target:exe /out:"Play JAMT.exe" launcher\PlayJAMT.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class PlayJAMT
{
    static string root;
    static readonly Dictionary<string, string> Types = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) {
        { ".html", "text/html; charset=utf-8" }, { ".htm", "text/html; charset=utf-8" },
        { ".js", "text/javascript; charset=utf-8" }, { ".mjs", "text/javascript; charset=utf-8" },
        { ".css", "text/css; charset=utf-8" }, { ".json", "application/json" },
        { ".md", "text/plain; charset=utf-8" }, { ".txt", "text/plain; charset=utf-8" },
        { ".jpg", "image/jpeg" }, { ".jpeg", "image/jpeg" }, { ".png", "image/png" },
        { ".gif", "image/gif" }, { ".webp", "image/webp" }, { ".svg", "image/svg+xml" }, { ".ico", "image/x-icon" }
    };

    static void Main()
    {
        Console.Title = "Junior Astronaut Mission Trainer";
        string exeDir = AppDomain.CurrentDomain.BaseDirectory;
        root = Path.GetFullPath(Directory.Exists(Path.Combine(exeDir, "mockups")) ? Path.Combine(exeDir, "mockups") : exeDir);
        if (!File.Exists(Path.Combine(root, "index.html")))
        {
            Console.WriteLine("Could not find the game (mockups\\index.html) next to this program.");
            Console.WriteLine("Unzip the whole folder first, then run Play JAMT.exe from inside it.");
            Console.WriteLine("Press Enter to close.");
            Console.ReadLine();
            return;
        }

        var listener = new TcpListener(IPAddress.Loopback, 0);   // 0 = any free port, only reachable from this PC
        listener.Start();
        int port = ((IPEndPoint)listener.LocalEndpoint).Port;
        string url = "http://127.0.0.1:" + port + "/";

        Console.WriteLine();
        Console.WriteLine("  JUNIOR ASTRONAUT MISSION TRAINER  (preview build)");
        Console.WriteLine("  -------------------------------------------------");
        Console.WriteLine("  The game is running at " + url);
        Console.WriteLine("  Your browser should open by itself. If it doesn't, copy that address into it.");
        Console.WriteLine();
        Console.WriteLine("  Keep this window open while you play. Close it to stop the game.");
        Console.WriteLine();

        try { Process.Start(new ProcessStartInfo(url) { UseShellExecute = true }); }
        catch { Console.WriteLine("  (Couldn't open the browser automatically.)"); }

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            ThreadPool.QueueUserWorkItem(_ => Handle(client));
        }
    }

    static void Handle(TcpClient client)
    {
        using (client)
        {
            try
            {
                client.ReceiveTimeout = 5000;
                NetworkStream stream = client.GetStream();
                var reader = new StreamReader(stream, Encoding.ASCII);
                string requestLine = reader.ReadLine();
                if (string.IsNullOrEmpty(requestLine)) return;
                string line;
                while (!string.IsNullOrEmpty(line = reader.ReadLine())) { }   // skip headers

                string[] parts = requestLine.Split(' ');
                if (parts.Length < 2 || (parts[0] != "GET" && parts[0] != "HEAD")) { Send(stream, 405, "text/plain", Encoding.ASCII.GetBytes("Method not allowed"), true); return; }

                string path = parts[1];
                int q = path.IndexOfAny(new[] { '?', '#' });
                if (q >= 0) path = path.Substring(0, q);
                path = Uri.UnescapeDataString(path).TrimStart('/');
                if (path == "") path = "index.html";

                string full = Path.GetFullPath(Path.Combine(root, path.Replace('/', Path.DirectorySeparatorChar)));
                if (!full.StartsWith(root, StringComparison.OrdinalIgnoreCase) || !File.Exists(full))
                {
                    Send(stream, 404, "text/plain", Encoding.ASCII.GetBytes("Not found"), parts[0] == "GET");
                    return;
                }
                string type;
                if (!Types.TryGetValue(Path.GetExtension(full), out type)) type = "application/octet-stream";
                Send(stream, 200, type, File.ReadAllBytes(full), parts[0] == "GET");
            }
            catch { /* browser closed the connection; ignore */ }
        }
    }

    static void Send(Stream s, int code, string type, byte[] body, bool includeBody)
    {
        string status = code == 200 ? "OK" : code == 404 ? "Not Found" : "Method Not Allowed";
        string head = "HTTP/1.1 " + code + " " + status + "\r\n" +
                      "Content-Type: " + type + "\r\n" +
                      "Content-Length: " + body.Length + "\r\n" +
                      "Cache-Control: no-cache\r\n" +
                      "Connection: close\r\n\r\n";
        byte[] h = Encoding.ASCII.GetBytes(head);
        s.Write(h, 0, h.Length);
        if (includeBody) s.Write(body, 0, body.Length);
        s.Flush();
    }
}
