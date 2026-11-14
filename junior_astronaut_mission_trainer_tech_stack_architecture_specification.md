# Junior Astronaut Mission Trainer (JAMT)
### Technical Architecture, Data Schemas & Engineering Specifications

*Target Event: NASA International Space Apps Challenge*  
*Document Version: 1.0.0*  
*Role: Machine-Readable Context & Implementation Blueprint for Developers and AI Coding Engines*

---

## 1. Technical Philosophy & Architectural Invariants

To eliminate common hackathon failure modes—such as unhandled promise rejections, CORS blocks during judging, state synchronization drift, and bloated asset bundles—every engineering choice in this project obeys four non-negotiable invariants:

1. **Deterministic State Progression (Pure Reducer Simulation):**  
   The simulation state is fully deterministic. Given a state $S_t$ and a player decision $D$, the next state $S_{t+1}$ is produced via a pure mathematical function. There are no opaque hidden variables.
2. **Zero-Failure Client Availability:**  
   The application must be $100\%$ playable completely offline or in low-bandwidth environments. If NASA API endpoints are down, rate-limited, or blocked by firewalls, the application functions seamlessly using curated, verified historical datasets embedded in the client bundle.
3. **Dual-Tier Dynamic Projection (Cadet vs. Specialist):**  
   The underlying engine does not fork into two applications. A single mathematical state model drives both the **Junior Cadet (Ages 7–12)** view and the **Senior Specialist (Ages 13+ / Classroom)** view. Switching tiers is a visual projection layer change, not a logic branch.
4. **Instant Zero-Config Playability:**  
   Zero logins, zero database migrations, zero client-side native builds. A browser accessing the root URL must boot into an interactive cockpit in under 800ms.

---

## 2. Chosen Technology Stack & Structural Defense

### 2.1 Core Framework: React 18+ (Vite)
* **Choice:** React 18+ bundled with Vite (`npm create vite@latest jamt-app --template react`).
* **Why this choice:**  
  * *Vite over Create-React-App:* Vite provides instant Hot Module Replacement (HMR) using native ES modules. In a 48-hour sprint, sub-second feedback loops save cumulative hours.
  * *Vite SPA over Next.js (SSR):* Next.js introduces serverless function cold starts, client/server boundary bugs (`use client`), and hydration mismatches. Because JAMT is an interactive client-side simulator, an SPA eliminates deployment edge-case failures while delivering identical performance.
* **Why not a Canvas Game Engine (Phaser / Pixi / Three.js):**  
  * The challenge brief explicitly demands communicating real systems trade-offs and accessibility for 7–12 year olds. Canvas engines make responsive typography, accessible screen readers, modular cards, and telemetry gauges expensive to build and brittle across mobile viewports.
  * Building a UI in DOM/Tailwind takes minutes per component. Canvas game scenes bottle up around singular monolithic loops that cause severe Git merge conflicts among a 6-person team.

### 2.2 Styling & Micro-Interactions: Tailwind CSS + Framer Motion + Lucide React
* **Choices:**
  * **Tailwind CSS:** Utility-first atomic styling.
  * **Framer Motion:** Declarative animations for incident card transitions, Sol advancement indicators, and telemetry stress-pulsing.
  * **Lucide React:** Lightweight, accessible SVG space/system icons (`Zap`, `ShieldAlert`, `Wind`, `Flame`, `Database`).
* **Why this choice:**  
  * Eliminates CSS naming bikeshedding and cross-file cascade collisions.
  * Framer Motion makes card swipes and gauge alarms feel like native hardware controls without the cognitive load or weight of a full physics engine.

### 2.3 State Management: Zustand
* **Choice:** Zustand (`create` store pattern).
* **Why this choice:**  
  * Provides a lightweight, boilerplate-free state container. Unlike Redux Toolkit, it requires zero providers wrapping the root, zero slice configurations, and zero dispatch ceremony.
  * Allows decoupled reading/writing of simulation parameters across isolated components (e.g., the telemetry header can listen to battery state without re-rendering the entire dialogue card component).

### 2.4 CI/CD & Edge Hosting: Vercel + GitHub Continuous Deployment
* **Choice:** Vercel integrated directly with GitHub `main` branch.
* **Why this choice:**  
  * Automated preview deployments for pull requests allow narrative leads, designers, and project managers to test live builds on mobile devices before merging code.
  * Global edge CDN caching guarantees that judges anywhere in the world experience instant load times.

---

## 3. System Architecture & Component Hierarchy

```
[ Root App: App.jsx ]
       │
       ├─► [ TopBar HUD ] ── (Zustand: Sol, Day/Night Cycle, Mode Toggle)
       │
       ├─► [ Telemetry Matrix ]
       │         ├─► Power System (kW / Gauge)
       │         ├─► Life Support (O2 / H2O Recycling)
       │         └─► Radiation Shielding (mSv Dosimeter)
       │
       ├─► [ Mission Control Center (Active Viewport) ]
       │         ├─► [ Incident Card Deck ] ── (Renders Active Dilemma)
       │         ├─► [ Data Traceability Drawer ] ── (NASA DONKI / OSDR Provenance)
       │         └─► [ Fail-Forward Post-Mortem Modal ] ── (On Critical Depletion)
       │
       └─► [ Static Data Engine / Cache Layer ]
                 ├─► Curated Scenarios JSON (Fallback & Primary)
                 └─► Non-Blocking Live DONKI Poller (Optional Hook)
```

---

## 4. Mathematical Simulation Model

The core engine computes state changes per shift/Sol using the following discrete equations:

### 4.1 Resource System State Vector

$$\mathbf{S}_t = \begin{bmatrix} P_t \\ L_t \\ S_t \\ R_t \end{bmatrix}$$

Where:
* $P_t \in [0, 100]$: Outpost Power Reserve (Battery/Generator percentage)
* $L_t \in [0, 100]$: Life Support & ECLSS Buffer (Atmosphere and recycled water viability)
* $S_t \in [0, 100]$: Habitat Shielding / Structural Integrity
* $R_t \ge 0$: Cumulative Crew Radiation Dosage (measured in $\text{mSv}$)

### 4.2 Environmental Drivers (Diurnal Cycle & Solar Activity)

For Sol step $t$:
* **Daylight Factor** $D(t) \in \{0, 1\}$ (or sinusoidal for Mars):
  * Lunar: $14\text{ Sols}$ Daylight ($D=1$), followed by $14\text{ Sols}$ Darkness ($D=0$).
  * Mars: Diurnal cycle with solar attenuation factor $\eta \approx 0.43$ due to orbital distance.
* **Solar Radiation Flux** $\Phi(t)$ drawn from NASA DONKI flare classification:
  $$\Phi(t) = \begin{cases} 
  1.0 & \text{Normal baseline} \\ 
  2.5 & \text{C-Class Solar Flare} \\ 
  6.0 & \text{M-Class CME Impact} \\ 
  15.0 & \text{X-Class Severe Particle Event} 
  \end{cases}$$

### 4.3 State Transition Function

When a player selects an action $A_k$, the transition to $\mathbf{S}_{t+1}$ is computed as:

$$P_{t+1} = \mathrm{clamp}\Big(P_t + (P_{\text{gen}} \cdot D(t)) - P_{\text{baseline}} + \Delta P(A_k), \; 0, \; 100\Big)$$

$$L_{t+1} = \mathrm{clamp}\Big(L_t - L_{\text{consumption}} + \Delta L(A_k) - \mathbf{1}_{\{P_t < 15\}} \cdot \Omega_{\text{life\_penalty}}, \; 0, \; 100\Big)$$

$$S_{t+1} = \mathrm{clamp}\Big(S_t - S_{\text{wear}} + \Delta S(A_k), \; 0, \; 100\Big)$$

$$R_{t+1} = R_t + \max\left(0, \; \frac{\Phi(t) \cdot \kappa_{\text{rad}}}{1 + \beta \cdot S_{t+1}}\right) + \Delta R(A_k)$$

* **Rule of Competing Cost:** Any action where $\Delta P(A_k) > 0$ must enforce $\Delta L(A_k) < 0$ or require spending mission consumable buffers.
* **Fail Condition:** A mission failure is reached if $P_{t+1} = 0$, $L_{t+1} = 0$, or $R_{t+1} \ge R_{\text{threshold}}$ (where $R_{\text{threshold}} = 1000\text{ mSv}$ career astronaut limit, scaled down for gameplay to $100\text{ mSv}$).

---

## 5. Standardized Data Schemas

Any scenario fed into the application must strictly adhere to the following schema structure. This ensures the UI renders predictably and attribution data links directly to authentic NASA archives.

### 5.1 Scenario / Incident Data Contract (`scenario.schema.json`)

```json
{
  "$schema": "http://json-schema.org/draft-07/schema#",
  "title": "MissionIncident",
  "type": "object",
  "required": [
    "id",
    "title",
    "location",
    "nasa_provenance",
    "cadet_narrative",
    "specialist_narrative",
    "choices"
  ],
  "properties": {
    "id": { "type": "string" },
    "title": { "type": "string" },
    "location": { "type": "string", "enum": ["Moon (Shackleton Rim)", "Mars (Jezero Crater)"] },
    "nasa_provenance": {
      "type": "object",
      "required": ["dataset", "record_id", "scientific_context", "external_url"],
      "properties": {
        "dataset": { "type": "string", "enum": ["DONKI", "OSDR", "Artemis III Surface Profile", "Curiosity RAD"] },
        "record_id": { "type": "string" },
        "scientific_context": { "type": "string" },
        "external_url": { "type": "string", "format": "uri" }
      }
    },
    "cadet_narrative": {
      "type": "object",
      "required": ["summary", "alert_level"],
      "properties": {
        "summary": { "type": "string" },
        "alert_level": { "type": "string", "enum": ["advisory", "warning", "critical"] }
      }
    },
    "specialist_narrative": {
      "type": "object",
      "required": ["telemetry_brief", "engineering_parameters"],
      "properties": {
        "telemetry_brief": { "type": "string" },
        "engineering_parameters": { "type": "string" }
      }
    },
    "choices": {
      "type": "array",
      "minItems": 2,
      "maxItems": 3,
      "items": {
        "type": "object",
        "required": ["id", "label_cadet", "label_specialist", "delta", "tradeoff_explanation"],
        "properties": {
          "id": { "type": "string" },
          "label_cadet": { "type": "string" },
          "label_specialist": { "type": "string" },
          "delta": {
            "type": "object",
            "required": ["power", "life_support", "shielding", "radiation_mSv"],
            "properties": {
              "power": { "type": "number" },
              "life_support": { "type": "number" },
              "shielding": { "type": "number" },
              "radiation_mSv": { "type": "number" }
            }
          },
          "tradeoff_explanation": { "type": "string" }
        }
      }
    }
  }
}
```

### 5.2 Curated Event Example (`sample_event.json`)

```json
{
  "id": "INC-DONKI-2024-CME-01",
  "title": "Coronal Mass Ejection Impact",
  "location": "Moon (Shackleton Rim)",
  "nasa_provenance": {
    "dataset": "DONKI",
    "record_id": "2024-03-23T01:30:00-CME-001",
    "scientific_context": "Real Coronal Mass Ejection tracked by SOHO/LASCO C3 coronagraph with speed > 1200 km/s.",
    "external_url": "https://kauai.ccmc.gsfc.nasa.gov/DONKI/"
  },
  "cadet_narrative": {
    "summary": "A giant cloud of solar particles is shooting from the Sun toward our lunar base!",
    "alert_level": "critical"
  },
  "specialist_narrative": {
    "telemetry_brief": "Interplanetary shock arrival confirmed. Solar proton flux exceeding 100 MeV energy threshold.",
    "engineering_parameters": "Ambient radiation levels escalating to 45 mSv/hr without supplemental shielding."
  },
  "choices": [
    {
      "id": "choice_shelter",
      "label_cadet": "Retreat to Underground Regolith Bunker",
      "label_specialist": "Order Crew Evacuation to Subsurface Storm Shelter",
      "delta": {
        "power": -25,
        "life_support": -10,
        "shielding": 0,
        "radiation_mSv": 2
      },
      "tradeoff_explanation": "Regolith soil absorbed the radiation safely, but secondary life-support and emergency batteries were drained to keep the bunker warm."
    },
    {
      "id": "choice_magnetic_shield",
      "label_cadet": "Turn On Electric Deflector Shield",
      "label_specialist": "Energize Active Magnetic Deflection Array",
      "delta": {
        "power": -60,
        "life_support": 0,
        "shielding": -15,
        "radiation_mSv": 8
      },
      "tradeoff_explanation": "Deflectors prevented biological injury, but the massive power draw exhausted your main batteries just before the lunar night cycle."
    }
  ]
}
```

---

## 6. Project Directory Layout

```
junior-astronaut-mission-trainer/
├── index.html
├── package.json
├── vite.config.js
├── tailwind.config.js
├── src/
│   ├── main.jsx
│   ├── App.jsx
│   ├── assets/
│   │   ├── icons/            # SVG system indicators
│   │   └── audio/            # Tactile telemetry clicks and alarms (optional)
│   ├── data/
│   │   ├── curatedScenarios.json   # Verified historical NASA incidents
│   │   └── planetaryConstants.json # Moon & Mars daylight/temperature baselines
│   ├── store/
│   │   └── useMissionStore.js      # Zustand store driving simulation state
│   ├── services/
│   │   ├── nasaDonkiService.js     # Non-blocking live API client with fallback
│   │   └── simulationEngine.js     # State transition equations and clamping
│   └── components/
│       ├── layout/
│       │   ├── HeaderHUD.jsx       # Day counter, Sol progress, Mode switcher
│       │   └── FooterStatus.jsx    # Provenance attribution links
│       ├── telemetry/
│       │   ├── TelemetryBar.jsx    # Visual meter with cadet/specialist mode
│       │   └── DosimeterGauge.jsx  # Cumulative radiation monitor
│       ├── mission/
│       │   ├── IncidentDeck.jsx    # Active crisis card display
│       │   ├── DecisionButton.jsx  # Trade-off interaction controls
│       │   └── ProvenanceBadge.jsx # Direct link to NASA DONKI/OSDR sources
│       └── modals/
│           ├── DebriefModal.jsx    # Pedagogical "Fail-Forward" post-mortem
│           └── VictoryModal.jsx    # Successful Sol threshold completion
```

---

## 7. Non-Blocking Live Data Integration Strategy

To fulfill the "live API" bonus without risking outage during judging, use a resilient fallback pattern:

```javascript
// src/services/nasaDonkiService.js
import fallbackData from '../data/curatedScenarios.json';

const NASA_DONKI_ENDPOINT = 'https://api.nasa.gov/DONKI/CMEAnalysis?mostAccurateOnly=true&speed=500';

export async function fetchActiveSpaceWeather(apiKey = 'DEMO_KEY') {
  const controller = new AbortController();
  const timeoutId = setTimeout(() => controller.abort(), 2500); // 2.5s strict timeout

  try {
    const response = await fetch(`${NASA_DONKI_ENDPOINT}&api_key=${apiKey}`, {
      signal: controller.signal
    });
    clearTimeout(timeoutId);

    if (!response.ok) throw new Error(`HTTP status ${response.status}`);
    const data = await response.json();

    return {
      isLive: true,
      source: "NASA DONKI Real-Time Telemetry",
      data: transformDonkiToIncident(data[0])
    };
  } catch (error) {
    console.warn("Live telemetry unreachable; falling back to curated archive.", error.message);
    return {
      isLive: false,
      source: "NASA DONKI Verified Historical Archive (Curated)",
      data: fallbackData[0]
    };
  }
}
```

---

## 8. Anti-Hallucination Directives for Coding Engines

When working within this codebase, any code-generation engine must obey the following boundaries:

1. **No External UI Component Libraries:** Do not install MUI, AntD, or Chakra. All components are built with native JSX, Tailwind CSS, and Lucide icons.
2. **No Complex Backend Dependencies:** Do not scaffold Express, NestJS, Prisma, or Supabase. The application runs entirely within the client runtime.
3. **Never Remove Trade-Off Penalties:** Do not write choices that yield `+power` without a negative cost to `life_support`, `shielding`, or `budget`. Every choice must enforce a sacrifice.
4. **Preserve Mode Symmetrical Integrity:** Any addition to the `scenario.schema.json` must provide both `cadet_narrative` (plain language, visual) and `specialist_narrative` (quantitative, scientific units).