# Junior Astronaut Mission Trainer (JAMT)
### Team Execution Pipeline & 48-Hour Sprint Playbook
*Target: NASA International Space Apps Challenge*  
*Team Size: 6 Members | Format: Pre-Sprint Setup + Weekend Execution*

---

## How to Read This Document

Six people, four tracks, two phases (Pre-Hackathon Prep & Weekend Sprint).

Every task states **what it is**, **why it matters**, **who owns it**, and **its hard definition of done**.
- **The Pre-Sprint Phase (Thursday – Friday):** Build the foundation, scaffold the repo, extract datasets, and write scenario cards so Saturday 11:00 AM begins with execution, not brainstorming.
- **The Weekend Sprint (Saturday – Sunday):** Synchronized to the venue schedule, mentor checkpoints, hard freezes, submission deadlines, and live pitch sessions.

---

## 1. The Four Operational Tracks & Team Ownership

```
┌────────────────────────────────────────────────────────────────────────┐
│                        JAMT TRACK ARCHITECTURE                         │
├─────────────────────┬─────────────────────┬────────────────────────────┤
│ TRACK 1: ENGINE     │ Person 1 (Lead Dev) │ Mathematical Reducer,      │
│ (Simulation Logic)  │                     │ Zustand Store, Solar/Night │
├─────────────────────┼─────────────────────┼────────────────────────────┤
│ TRACK 2: COCKPIT    │ Person 2 (Frontend) │ React UI, Responsive HUD,  │
│ (UI & Interaction)  │ Person 3 (Frontend) │ Dynamic Telemetry, Framer  │
├─────────────────────┼─────────────────────┼────────────────────────────┤
│ TRACK 3: DATA & SCI │ Person 4 (Science)  │ NASA DONKI / OSDR Curation,│
│ (Scenarios & Copy)  │ Person 5 (Narrative)│ Incident Cards, Plain Lang │
├─────────────────────┼─────────────────────┼────────────────────────────┤
│ TRACK 4: PROOF      │ Person 6 (PM & Lead)│ 7-Slide Deck, Video Demo,  │
│ (Pitch & Delivery)  │                     │ Pitch Defense, Submission  │
└─────────────────────┴─────────────────────┴────────────────────────────┘
```

### Track Ownership Matrix:
* **Track 1: Engine & Systems (Person 1 - Systems Architect / Fullstack):**  
  Owns state management, the mathematical transition loop ($P, L, S, R$), clamped state boundaries, diurnal cycle timers, and the non-blocking NASA live-poller hook.
* **Track 2: Cockpit & Interface (Person 2 - Lead UI & Person 3 - Component/FX):**  
  Owns React layout, Tailwind component library, Lucide icons, Framer Motion transitions, responsive tablet/desktop views, and the Cadet/Specialist dual-mode projection toggle.
* **Track 3: Data & Mission Intelligence (Person 4 - Data Researcher & Person 5 - Narrative/Pedagogy):**  
  Owns NASA DONKI/OSDR historical data extraction, schema validation, drafting 15–20 crisis incident cards, ensuring authentic engineering trade-offs, and authoring "Fail-Forward" post-mortem debriefs.
* **Track 4: Proof, Pitch & Compliance (Person 6 - Project Manager / Pitch Anchor):**  
  Owns the Space Apps submission portal, 3-minute pitch deck, script, video demo recording, timekeeping against hackathon milestones, and mentor review prep.

---

## 2. Team Operating Rules (Hackathon Laws)

1. **The Main Branch Never Breaks:** Feature branches only (`feat/hud-gauges`, `feat/donki-loader`). Nothing merges to `main` without running `npm run build` locally with zero errors.
2. **Never Invent Data When NASA Has It:** Every incident card must carry an authentic NASA dataset provenance link (DONKI event ID, OSDR study ID, or Artemis profile).
3. **No Choice Is Free:** Any developer or narrative writer introducing a decision card that gives a benefit without a penalty will have that card rejected.
4. **Daily Integration Check (Syncs):** Standups are 10 minutes maximum. Answer only three questions:
   - What did you ship since the last check-in?
   - What are you committing to in the next 3 hours?
   - What blocker is stopping you, and who in the team fixes it?
5. **Hard Code Freeze at Sunday 11:30 AM:** No new features after 11:30 AM on Sunday. The remaining 2.5 hours are strictly for testing, the 30-second video demo, slide deck rehearsals, and portal upload.

---

## 3. PHASE 0: Pre-Hackathon Setup (Thursday – Friday)
*Goal: Arrive Saturday morning with a deployed blank app, locked schemas, and curated JSON cards.*

### Tasks for Thursday – Friday:

#### Track 1 (Engine) & Track 2 (Cockpit) — Shared Setup
* **Scaffold Repository:** Create Git repository with Vite + React + Tailwind CSS + Lucide React + Framer Motion.
* **CI/CD Pipeline:** Deploy blank template to Vercel. Verify that commits to `main` trigger a live deployment within 45 seconds.
* **Store Skeleton:** Implement basic Zustand store (`useMissionStore.js`) containing default state:
  $$\mathbf{S}_0 = \{ P: 100, L: 100, S: 100, R: 0, \text{sol}: 1, \text{mode}: \text{'cadet'} \}$$
* **Component Wireframes:** Code blank shells for `<HeaderHUD />`, `<TelemetryMatrix />`, `<IncidentDeck />`, and `<DebriefModal />`.

#### Track 3 (Data & Mission Intelligence)
* **Dataset Mining:**
  - Locate 5 verified historical Coronal Mass Ejections (CMEs) & Solar Flare events from NASA DONKI.
  - Locate 3 radiation/biological data points from NASA OSDR (e.g., DNA repair strain, bone density limits).
  - Locate Artemis/Apollo surface temperature and day/night parameters for Lunar South Pole.
* **Curate `scenarios.json`:** Author at least 8 balanced incident cards matching the `scenario.schema.json` format defined in the Architecture Doc.

#### Track 4 (Proof & Pitch)
* **Space Apps Portal Setup:** Complete team profile, claim team name, add all 6 member accounts, and select the *"Junior Astronaut Mission Trainer"* challenge track.
* **Slide Deck Skeleton:** Create the 7-slide pitch deck structure in Google Slides/Canva following the 3-minute pitch criteria.

---

## 4. PHASE 1: Saturday Hackathon Pipeline

### 08:00 – 10:00 | Arrival, Setup & Pre-Flight Check
* Check in, configure venue Wi-Fi, verify power connections.
* Verify local Node.js environments and ensure all 6 members can clone and build the repo.
* Attend Official Launch & Challenge Briefing (10:00 – 11:00).

```
11:00 SATURDAY — HACKING OFFICIALLY BEGINS
```

### 11:00 – 13:00 | Sprint Block 1: The Core Loop
* **Track 1 (Engine):** Implement the reducer transition equations in `simulationEngine.js`. Add boundary clamping and death condition triggers ($P \le 0$, $L \le 0$, $R \ge 100$).
* **Track 2 (Cockpit):** Build `<TelemetryBar />` and `<DosimeterGauge />`. Implement fluid visual color-shifts (Green $\to$ Yellow $\to$ Red) and animated meter drains.
* **Track 3 (Data):** Expand scenario catalog from 8 to 15 cards. Add Mars dust storm events (Opportunity/Curiosity data) and thermal night drops.
* **Track 4 (Pitch):** Draft the 30-second Problem Pitch script: *"Why do existing tools fail kids, and how does real NASA data solve this?"*

---

### 12:00 | MENTOR ROUND ONE (Check-in Checklist)
**Team Goal:** Deliver a 90-second crisp elevator pitch to the visiting mentor:
1. State the challenge and target audience (Ages 7–12 with teen/classroom flexibility).
2. Show the three core systems: Power, Life Support, Radiation.
3. Show the JSON schema linking an event directly to a NASA DONKI record.
4. Ask the mentor for feedback on judging presentation clarity.

---

### 13:00 – 14:00 | Lunch Break (Mandatory step away from screen)

---

### 14:00 – 17:00 | Sprint Block 2: Interactive Deck & Dual Projection
* **Track 1 (Engine):** Connect Zustand store to incident progression. Handle Sol cycle advancement: Day ($D=1$) vs. Night ($D=0$) power drains.
* **Track 2 (Cockpit):** Build `<IncidentDeck />` with interactive choice buttons. Implement Framer Motion swipe/slide animations when an action is selected.
* **Track 2 (Cadet/Specialist Toggle):** Implement UI toggle that switches labels from qualitative icons to exact engineering units (kW, mSv, kPa).
* **Track 3 (Data):** Author the `<DebriefModal />` content. For every failure mode, write an educational, constructive telemetry diagnosis (e.g., *"Your batteries ran dry on Sol 3 because..."*).
* **Track 4 (Pitch):** Script the 30-second to 2-minute project video. Collect in-game screen recordings of working components.

---

### 17:00 | MENTOR ROUND TWO (Progress & Validation)
**Team Goal:** Show working software on a live browser URL:
1. Walk the mentor through 3 full turns (Sols).
2. Trigger an intentional failure to demonstrate the "Fail-Forward" Telemetry Debrief.
3. Show the NASA data attribution drawer proving traceability to open datasets.

---

### 17:30 – 18:00 | Day One Integration & Wrap-Up
* **All Hands:** Full integration run on Vercel production deployment.
* Test responsiveness on a mobile phone / tablet.
* Commit code, push all branches, and log what is left for Sunday morning.

---

## 5. PHASE 2: Sunday Hackathon Pipeline

### 08:00 – 09:00 | Doors Open & Breakfast Sync
* Quick 10-minute standup: Review night commits, check Vercel deployment health.
* Lock down remaining tasks before the submission clinic.

### 09:00 – 10:00 | Submission Clinic & Pitch Strategy
* **Track 4 Lead (Person 6):** Attend the Submission Clinic with mentors. Fill out mandatory Space Apps portal fields:
  - Project Title & High-Level Summary
  - List of NASA Open Datasets used (DONKI, OSDR, Mars mission data)
  - Public GitHub repository link
* **Pitch Alignment:** Calibrate slides to the 3-minute pitch structure (30s Problem, 60s Solution/Data, 60s Demo, 30s Impact).

---

### 10:00 – 11:30 | Final Sprint: Polish & Fallback Hook
* **Track 1 (Engine):** Add non-blocking `fetchActiveSpaceWeather()` helper with 2.5s timeout. If live DONKI responds, display the "Live NASA Telemetry Connected" badge; otherwise, seamlessly maintain curated mode.
* **Track 2 (Cockpit):** Visual polish: audio sound bites on button clicks, subtle screen shake on radiation flare, high-contrast accessibility verification.
* **Track 3 (Data):** Proofread all card copy for plain-language clarity, ensuring no jargon is left unexplained for a 9-year-old cadet.
* **Track 4 (Proof):** Record the final screen capture demo video. Include voiceover explaining the trade-offs.

---

```
11:30 SUNDAY — CODE FREEZE (ALL TRACKS)
No new features or PRs merged. The codebase is locked.
```

---

### 11:30 – 13:00 | Video Finalization, Deck Polish & Verification
* **Track 4 & Narrative Lead:** Export and upload the final demo video (YouTube / Vimeo / Google Drive unlisted). Ensure the link is public and requires no login.
* **Frontend Lead:** Verify the production Vercel URL in an Incognito window, on mobile Safari, and Chrome.
* **Team Rehearsal:** Run through the 3-minute pitch deck twice. Time the presentation strictly to 2 minutes 50 seconds.

---

### 13:00 – 14:00 | Lunch Break & Final Portal Verification
* Verify all required submission fields on the NASA Space Apps platform.
* Double-check:
  - [x] Does the demo link open without login?
  - [x] Is the GitHub repo public?
  - [x] Are NASA open data sources clearly credited?

---

```
14:00 SUNDAY — OFFICIAL SUBMISSION DEADLINE
Submission uploaded and locked on Space Apps platform.
```

---

### 14:30 – 16:30 | Live Pitch Sessions & Judging Defense
* Deliver 3-minute pitch followed by 2 minutes of Q&A from the judges.
* **Judging Q&A Defense Strategy:**
  - *If asked about scientific accuracy:* Point to the NASA Provenance Drawer and exact DONKI flare event IDs embedded in the cards.
  - *If asked about accessibility for kids:* Demonstrate Cadet Mode vs. Specialist Mode with one click.
  - *If asked why this isn't a complex 3D sim:* Quote the challenge brief: *"A simple game that teaches a real constraint beats a beautiful one that doesn't."*

---

## 6. What to Cut If Behind Schedule (Emergency Protocol)

If development stalls by Saturday 17:00, cut tasks in this strict order:

1. **Cut First:** Live NASA API fetching (rely entirely on the pre-bundled `scenarios.json`).
2. **Cut Second:** Sound effects and audio cues.
3. **Cut Third:** Mars location (ship Moon / Shackleton Crater exclusively).
4. **Cut Fourth:** Advanced card animations (use standard instant transitions).

**NEVER CUT:**
- The 3-resource balance loop ($P, L, S$).
- The "Fail-Forward" Telemetry Debrief on failure.
- NASA data citations and provenance links.
- Instant, login-free browser availability.