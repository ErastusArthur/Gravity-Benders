# Junior Astronaut Mission Trainer (JAMT)
### System Context, Product Vision & Challenge Blueprint
*Prepared for the NASA International Space Apps Challenge*

---

## 1. The Core Problem
Most space-themed STEM educational media suffers from a fundamental binary failure:
1. **The Sci-Fi Fantasy Trap:** Media oversimplifies survival into frictionless exploration or clicker mechanics with unlimited resources, ignoring basic physical constraints (thermodynamics, radiation dosage, orbital daylight cycles).
2. **The Textbook Trap:** Tools present real space operations as dense, high-barrier telemetry spreadsheets and specialized academic language, quickly alienating younger students.

The consequence is that young learners understand space outcomes ("we landed on Mars," "we set up a habitat"), but have zero intuition for **mission trade-off engineering**. 

In actual human spaceflight, no decision is free. Every watt allocated to high-gain Earth communications is a watt stolen from life support scrubbers. Every hour an astronaut spends on an EVA (extravehicular activity) science excursion carries a cumulative radiation tax. When young students fail to encounter these competing constraints, they miss the defining intellectual challenge of aerospace engineering.

---

## 2. What We Are Building
**Junior Astronaut Mission Trainer (JAMT)** is an interactive, browser-based outpost management simulator built for **students aged 7–12**. 

It puts learners directly into the commander's seat of a long-duration lunar or Martian surface base. Rather than testing dexterity or memorization, the simulator presents daily, consequential operational dilemmas driven by **authentic NASA environmental data**. 

Students manage a compact, interdependent matrix of critical life-support systems. When environmental crises strike—such as Coronal Mass Ejections (CMEs) or severe regolith dust accumulation—players must choose which systems to preserve, which to throttle, and how to absorb compound risks over time.

---

## 3. Core Design Principles (AI & Developer Guardrails)

### Principle I: "No Free Choices"
Every actionable decision must involve a tangible trade-off. There are no "strictly optimal" answers. If an action preserves crew health, it must cost power reserves or scrub science yield. If an action saves energy, it must impose thermal instability or delay life support recycling. 

### Principle II: Two to Three Core Resources (Cognitive Load Control)
The target audience is 7–12 years old. While real mission telemetry tracks hundreds of metrics, JAMT isolates **three interconnected primary systems**:
1. **Power Budget ($P$):** Battery storage, solar array output, auxiliary reactors (sensitive to diurnal/night cycles and dust).
2. **Life Support & Atmosphere ($L$):** Oxygen concentration, water recycling efficiency, carbon dioxide scrubbing (ECLSS dynamics).
3. **Shielding & Crew Health ($S$):** Regolith mass shielding, magnetic deflector buffers, cumulative radiation exposure (millisieverts).

### Principle III: Transparent Grounding in NASA Open Data
Game numbers and scenario triggers are not arbitrary; they reflect real physical data provided by NASA and planetary science archives:
* **Space Weather:** Space Weather Database Of Notifications, Knowledge, and Information (**DONKI**) — real historical Solar Energetic Particle (SEP) storms and solar flares.
* **Biological Limits & Dosimetry:** Open Science Data Repository (**OSDR**) — real radiation dosage thresholds and crew biological impacts.
* **Surface Realism:** Real diurnal cycles (e.g., the 14-day lunar night, Martian day/night sol duration, solar irradiance drop-off at Mars).

### Principle IV: "Fail Forward" Feedback Loop (No Game-Over Screens)
Failure is the most potent pedagogical tool in engineering. If a player depletes their battery or radiation threshold:
* The system **never** displays a generic "Game Over / You Died" screen.
* Mission Control conducts an **Automated Telemetry Debrief**: *"Telemetry shows your batteries depleted on Night 4 because you ran heaters and communications simultaneously. NASA engineers solve this by pre-storing heat in thermal masses or entering low-power hibernation. Adjust your power grid and try again."*

---

## 4. System Mechanics & User Experience

### A. The Turn / Sol Loop
* The simulation operates on discrete day cycles (**Sols** for Mars or **Lunar Shifts** for the Moon).
* Each shift presents an environmental state influenced by real data (e.g., solar flux index, ambient surface temperature, storm alert).
* The player evaluates the status of the three core bars ($P, L, S$) and resolves a primary operational crisis card.

### B. Crisis Event Archetypes (Examples Grounded in Data)
1. **Incoming Solar Particle Event (DONKI Data):**
   * *Option A (Safe/Resource Cost):* Retract crew into subterranean regolith storm shelter. 
     * *Trade-off:* Shields protected ($0\text{ mSv}$ dose), but habitat power must reroute to auxiliary shelter life support ($-20\%\text{ Power}$), halting all active surface science experiments.
   * *Option B (Risk/Efficiency):* Maintain habitat perimeter with high-frequency electromagnetic shields.
     * *Trade-off:* Science continues ($+10\text{ Science}$), but drains main battery bank by $45\%$ right before the night cycle begins.
2. **Dust Accumulation on Photovoltaic Arrays:**
   * *Option A:* Vent compressed air to blow off dust.
     * *Trade-off:* Restores solar generation efficiency to $100\%$, but expends consumable oxygen/gas reserves ($-15\%\text{ Life Support}$).
   * *Option B:* Accept degraded solar generation ($40\%$ deficit) and throttle non-essential systems until a robotic sweep pass.

### C. Language & Accessibility
* **Scientific terms are explained in context:** Words like *mSv (millisieverts)*, *regolith*, and *sol* are accompanied by plain-language definitions and intuitive visual icons.
* **Zero friction:** Pure client-side browser delivery. No download, no installation, no logins.

---

## 5. What Makes This Solution Stand Out to NASA Judges

| Dimension | Typical Submissions (Common Pitfalls) | Junior Astronaut Mission Trainer |
| :--- | :--- | :--- |
| **Data Realism** | Arbitrary video game stats (Health: 100 HP, Mana: 50). | Grounded in DONKI solar flare scales (C, M, X-class) and actual surface flux rates. |
| **Game Balance** | Obvious "right" choices where kids just click the green button. | Strict trade-offs where every mitigation requires sacrificing another system. |
| **Educational Impact** | Passive reading quiz or trivia game. | Active systems-thinking simulator: students experience the cause-and-effect cascade of resource budgeting. |
| **Pedagogy** | Punitive failure that discourages replay. | Constructive mission debriefs explaining the engineering remedy. |
| **Accessibility** | Over-engineered desktop apps requiring installation. | Instant, high-contrast, zero-barrier browser experience playable on tablets, laptops, and smartboards. |

---

## 6. Prompting Context & Behavior Rules for AI Assistants

When assisting in the ideation, scriptwriting, system balancing, or architecture of this project:
1. **Never introduce magical or fantasy sci-fi tropes** (e.g., forcefields without massive power costs, teleportation, infinite oxygen generators).
2. **Never suggest safe/unpunished choices.** Always ensure any suggestion or event card includes a structural cost.
3. **Respect the target age range (7–12).** Keep explanations punchy, dynamic, and free of unnecessary academic jargon, but never speak down to the learner.
4. **Enforce data attribution.** Always connect simulated space weather or planetary surface anomalies back to real NASA open data assets (DONKI, OSDR, Artemis/Mars datasets).