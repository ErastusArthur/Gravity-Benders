JUNIOR ASTRONAUT MISSION TRAINER - PREVIEW BUILD
================================================

This is an early test version of our NASA Space Apps game. The real one is still being built!

HOW TO PLAY (Windows)
1. Unzip the whole folder first. (Running it from inside the zip won't work.)
2. Double-click "Play JAMT.exe".
3. Your web browser opens the game. Keep the black window open while you play.
   Close that window when you're done.

WINDOWS SECURITY WARNING
Windows may say "Windows protected your PC" because this program isn't signed
by a big company. Click "More info", then "Run anyway".
The program only runs a tiny web server on your own computer (127.0.0.1) so the
game can load. Nothing is reachable from the internet. Its source code is in
launcher\PlayJAMT.cs if you want to check.

INTERNET
The 3D view downloads a graphics library the first time, so you need internet.
Without it, the game switches to the flat photo map automatically.

ON A MAC OR LINUX?
The .exe won't run. Instead, open a terminal in this folder and run:
    python3 -m http.server 5173 --directory mockups
then open http://localhost:5173 in your browser.

WHAT TO TRY
- Tap places on the map to send the rover or crew out.
- Tap "Base" to go inside and use the greenhouse, workshop and storm shelter.
- Switch between Cadet (simple) and Specialist (real units) views.
- Press "Storm today (demo)" and see what happens to anyone outside!

Tell us what was fun, what was confusing, and anything that broke.
