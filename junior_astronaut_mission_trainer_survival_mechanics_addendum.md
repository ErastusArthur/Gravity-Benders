# Junior Astronaut Mission Trainer (JAMT)
### Addendum: Survival-Game Mechanics (Inspired by the Genre, Grounded in NASA Reality)

*Companion to the Product Vision, Tech Spec, and Team Pipeline docs.*

---

## 0. Why This Addendum Exists

Survival RPGs (the genre of games like *Day R Survival*) are great at one thing JAMT needs: **making players feel that every resource is scarce and every trip has a cost.** Players in that genre plan routes, ration supplies, fix broken gear, and dread the next storm.

This addendum adapts those **mechanic patterns** (not any game's setting, story, art, or names) into JAMT's space setting. Every idea below follows the existing rules:

- **No Free Choices.** Every new action costs Power ($P$), Life Support ($L$), Shielding ($S$), or Radiation ($R$).
- **Three core bars stay three.** New systems feed into $P$, $L$, $S$, $R$. They don't add new primary bars.
- **Real NASA grounding.** Each mechanic names the real mission or dataset it comes from.
- **Fail Forward.** New failure modes get a debrief, never a "Game Over".
- **Ages 7–12.** Each mechanic should be explainable in one sentence to a 9-year-old.

---

## 1. Mechanics Overview & Scope

| # | Mechanic | Survival-genre pattern | JAMT version | Hackathon scope |
| :- | :--- | :--- | :--- | :--- |
| 1 | **Spare Parts & Repairs** | Crafting and fixing worn gear | Small crate of spare parts used to repair systems or build upgrades | **MVP** |
| 2 | **Greenhouse Module** | Hunting and cooking food | Grow plants that use power and water but make oxygen and food | **MVP** |
| 3 | **Cargo Mass Budget** | Heavy inventory slows you down | Every kilogram on a supply lander or rover costs power | **MVP (light)** |
| 4 | **Rover Expeditions** | Traveling a big map between towns | Short rover trips to nearby science sites, with radiation and power costs | Stretch |
| 5 | **Crew Condition Tags** | Illness, injury, fatigue status effects | Each astronaut has one condition (Rested / Tired / Radiation-Sick) | Stretch |
| 6 | **Supply Lander Windows** | Rare traders and supply caches | Resupply only arrives on real launch-window schedules | Stretch |
| 7 | **Mission Log & Milestones** | Quest journal | A captain's log that tracks goals (survive the lunar night, grow first lettuce) | **MVP (copy only)** |

**MVP rule:** if the team is behind, ship #1, #2, and #7 only. They each touch the existing card schema lightly and add the most "survival feel" per hour of work.

---

## 2. Mechanic Details

### 2.1 Spare Parts & Repairs (MVP)

**Kid-level pitch:** *"You only brought a few spare parts from Earth. Use them wisely. When they're gone, you can't fix things anymore."*

**How it works**
- New secondary counter: **Spare Parts** `K ∈ {0..5}`. It's shown as small crate icons, not a full bar.
- Some choices on crisis cards cost spare parts instead of (or in addition to) a bar.
- A few cards offer a **Fabricate** option: turn regolith (moon dirt) into shielding bricks or 3D-print a replacement part, at a power cost.

**Trade-off rules**
- Repairs with parts save a bar now but leave you with fewer parts for later. The danger is *future* crises you can't fix.
- Fabricating parts costs a lot of power ($\Delta P \le -20$), so it's only practical during daylight.

**Example card choices**
- *Solar panel hinge jammed:*
  - **Use 1 spare part:** $K - 1$, power generation restored.
  - **Leave it:** Panel stays at 60% output for the next 3 Sols.
- *Regolith brick press:*
  - **Bake bricks in solar furnace:** $\Delta P = -25$, $\Delta S = +15$.

**NASA grounding**
- ISS crews rely on a limited stock of spares and on-orbit 3D printing (NASA's In-Space Manufacturing project).
- NASA research into turning lunar regolith into building material and shielding.

**Fail Forward debrief example**
> "You ran out of spare parts on Sol 9, so the broken air pump couldn't be fixed. Real missions carry carefully chosen spares and plan which systems are most likely to break first."

---

### 2.2 Greenhouse Module (MVP)

**Kid-level pitch:** *"Plants need light and water, but they give you fresh air and food back."*

**How it works**
- The player can switch the **Greenhouse** ON or OFF (one toggle on the main screen).
- When ON, each Sol:
  - Costs power for grow lights: $\Delta P = -8$.
  - Helps life support with oxygen and food: $\Delta L = +5$.
- Plants need **5 Sols of steady power** before the first harvest. If power drops below 15%, the plants wilt and the counter resets.

**Trade-off rules**
- During the lunar night, the greenhouse can drain the batteries fast. Players must decide whether to keep it on or let the plants die.
- A harvest gives a one-time $\Delta L = +15$ bonus. That rewards long-term planning over short-term power saving.

**NASA grounding**
- The **Veggie** plant-growth system on the ISS, where astronauts grew and ate lettuce.
- **MOXIE** on the Perseverance rover, which showed that oxygen can be made from Mars air (a possible extra "Oxygen Maker" card with a high power cost).

**Fail Forward debrief example**
> "Your greenhouse wilted on Sol 6 because the batteries dropped too low at night. Real space farmers protect their plants by saving power before dark."

---

### 2.3 Cargo Mass Budget (MVP, light version)

**Kid-level pitch:** *"Every kilogram you send to space costs fuel. Pack smart!"*

**How it works**
- At mission start, the player picks a **loadout** with a 100 kg budget (a simple pre-mission screen with 4–5 items):

| Item | Mass | Effect at start |
| :--- | :--- | :--- |
| Extra battery pack | 40 kg | $P_0 = 100$ instead of 80 |
| Spare parts crate | 30 kg | $K_0 = 5$ instead of 2 |
| Water reserve tank | 30 kg | $L_0 = 100$ instead of 85 |
| Extra shielding panels | 40 kg | $S_0 = 100$ instead of 80 |
| Greenhouse seed kit | 10 kg | Greenhouse unlocked |

- Players can't take everything, so the first decision of the game is already a trade-off.

**NASA grounding**
- Launch cost scales with mass. Every kilogram to the Moon or Mars requires much more fuel and rocket mass (the rocket equation). Mission planners fight over every kilogram.

---

### 2.4 Rover Expeditions (Stretch)

**Kid-level pitch:** *"Drive the rover to a crater to find science, but the farther you go, the more power and radiation it costs."*

**How it works**
- A small map with **3–4 nearby sites** (e.g., crater rim, lava tube entrance, ice deposit in a shadowed crater).
- Each trip shows its cost before the player commits:
  - Power to drive: $\Delta P = -10$ per leg.
  - Radiation outside the habitat: $\Delta R = +2$ per Sol away (more during a flare).
  - Reward: **Science points**, or a resource find (water ice → $\Delta L$, salvageable hardware → $K + 1$).
- Rough terrain risks **wheel damage**, costing a spare part.

**Trade-off rules**
- Expeditions are the main source of science points (the "victory" score), so the player must leave the safe base to do well.
- A solar storm warning that arrives during an expedition forces a choice: rush back (extra power cost) or shelter in a lava tube (lose a Sol).

**NASA grounding**
- **Curiosity** rover wheel damage from sharp Mars rocks.
- Lunar south pole water ice targeted by Artemis.
- Lava tubes studied as natural radiation shelters.

---

### 2.5 Crew Condition Tags (Stretch)

**Kid-level pitch:** *"Your astronauts get tired and can get sick from radiation. Take care of them!"*

**How it works**
- Show 2–3 crew portraits. Each has **one** status tag at a time:
  - **Rested** (normal).
  - **Tired** (after 2 back-to-back expeditions): their next repair costs an extra Sol.
  - **Radiation-Sick** (when $R$ passes 50% of the limit): they can't go outside until they rest.
- **Rest Day** action: skip a Sol's science, but return a crew member to Rested. Base drains continue, so resting still costs $P$ and $L$.

**NASA grounding**
- **OSDR** studies on radiation effects and crew health.
- Real ISS crew schedules with mandatory rest and exercise time.

---

### 2.6 Supply Lander Windows (Stretch)

**Kid-level pitch:** *"Help from Earth only comes at certain times. Can you hold on until it arrives?"*

**How it works**
- Show a countdown: **"Next supply lander: Sol 12."**
- On arrival, the player picks **one** of three crates (power, life support, or spare parts). Again, no taking everything.
- Mars mode: resupply is rare, matching real Mars launch windows (roughly every 26 months), scaled down for gameplay.

**NASA grounding**
- Earth–Mars launch windows open about every 26 months because of planetary alignment.
- Lunar missions can be resupplied more often (a few days' travel), which is a good Moon vs. Mars comparison for students.

---

### 2.7 Mission Log & Milestones (MVP, copy only)

**Kid-level pitch:** *"Your captain's log tracks big goals for your base."*

**How it works**
- A simple checklist panel with 4–6 goals, for example:
  - Survive your first lunar night.
  - Harvest your first greenhouse crop.
  - Get through a solar storm without anyone getting radiation-sick.
  - Finish the mission with at least 1 spare part left.
- Completing a goal shows a short "Did you know?" NASA fact.
- Unmet goals are mentioned in the end debrief as things to try next time.

**Why it matters:** Survival games keep players going with goals, not just by avoiding failure. This also gives teachers natural discussion prompts.

---

## 3. Engine & Schema Changes

### 3.1 State Vector Extension

$$\mathbf{S}_t = \begin{bmatrix} P_t & L_t & S_t & R_t & K_t & G_t \end{bmatrix}^\top$$

- $K_t \in \{0..5\}$: Spare parts count.
- $G_t \in \{0..5\}$: Greenhouse growth progress (Sols of steady power). $G = -1$ means the greenhouse is locked or off.

Greenhouse update per Sol, when ON:

$$G_{t+1} = \begin{cases} 0 & \text{if } P_t < 15 \\ G_t + 1 & \text{otherwise} \end{cases}$$

On reaching $G = 5$: apply the harvest bonus and reset $G$ to 0.

### 3.2 Choice `delta` Additions (backward-compatible)

Add **optional** fields to each choice's `delta` in `scenario.schema.json`:

```json
"delta": {
  "power": -20,
  "life_support": 0,
  "shielding": 10,
  "radiation_mSv": 0,
  "spare_parts": -1,
  "science": 0,
  "sols_lost": 0
}
```

- `spare_parts`, `science`, and `sols_lost` default to `0` when missing, so existing cards keep working.
- **Updated "No Free Choices" check:** a choice whose effect is positive overall must have at least one negative value across `power`, `life_support`, `shielding`, `spare_parts`, `sols_lost`, or a positive `radiation_mSv`.
- A choice that costs a spare part when $K = 0$ is shown **disabled**, with the label "No spare parts left".

### 3.3 New Data Files

```
src/data/
├── loadoutItems.json        # Cargo mass budget options (2.3)
├── expeditionSites.json     # Rover destinations and costs (2.4, stretch)
└── milestones.json          # Mission log goals + "Did you know?" facts (2.7)
```

### 3.4 New Components

```
src/components/
├── mission/
│   ├── SparePartsCrate.jsx      # 0–5 crate icons
│   ├── GreenhouseToggle.jsx     # On/off + growth progress
│   └── MissionLog.jsx           # Milestone checklist
└── screens/
    ├── LoadoutScreen.jsx        # Pre-mission 100 kg packing
    └── ExpeditionMap.jsx        # Stretch: rover destinations
```

---

## 4. New Fail-Forward Debriefs

Track 3 should write a debrief for each new failure cause:

| Failure cause | Debrief should teach |
| :--- | :--- |
| Ran out of spare parts, then a key system broke | Why missions plan spare parts around the most likely failures |
| Greenhouse drained batteries at night | Saving power before dark; thermal and power planning |
| Packed the wrong loadout | Mass budgets and trade-offs before launch |
| Crew too much radiation on expeditions | Radiation dose limits, shelters, and timing EVAs around space weather |
| Ran out of supplies before the lander arrived | Launch windows and why Mars missions must be more self-sufficient than Moon missions |

---

## 5. Team Impact (fits the 48-hour pipeline)

| Track | Extra work for MVP mechanics (#1, #2, #3, #7) |
| :--- | :--- |
| **Track 1: Engine** | Add $K$ and $G$ to the store and reducer; schema defaults; disable choices when $K = 0$. ~2–3 hours. |
| **Track 2: Cockpit** | `SparePartsCrate`, `GreenhouseToggle`, `LoadoutScreen`, `MissionLog`. ~3–4 hours across two people. |
| **Track 3: Data** | Add `spare_parts` costs to 4–6 existing cards; write loadout items, milestones, and new debriefs. ~3 hours. |
| **Track 4: Pitch** | One slide line: *"Survival-game tension, real NASA trade-offs."* Show the loadout screen in the demo, since it's the fastest way to show trade-offs. |

**Updated cut order** (add to the Emergency Protocol in the Team Pipeline doc):
1. Cut stretch mechanics (#4, #5, #6) first, before anything in the original cut list.
2. Then cut the Loadout Screen (start everyone with a fixed default loadout).
3. Keep Spare Parts, the Greenhouse, and the Mission Log as long as possible. They're cheap to build and add the most survival feel.
