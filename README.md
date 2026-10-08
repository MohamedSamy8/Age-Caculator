# 🎂 Project Age Calculator Form

> Enter your birthday — get your age in years, months, and days, plus total time lived in every unit, the day of the week you were born on, and how many days until your next birthday.

---

<img width="1171" height="751" alt="Screenshot 2026-10-08 112511" src="https://github.com/user-attachments/assets/41449a9c-42da-41ed-a617-61b368401b08" />


---

## 🚀 Project Overview

Fourth project in the WinForms self-practice series.

Same four controls. Different problem.

This one takes a date and turns it into a full picture of how long you have been alive.

Three inputs: Day, Month, Year.

One button.

Results across four sections: your exact age, total time lived, day of birth, and next birthday countdown.

---

## 🏗️ Architecture Design

```
btnCalculateAge_Click
 ├── ClearResults()               ← reset previous results before recalculating
 └── ValidateBirthDate()
      ├── IsBirthDateEmpty()     → ShowBirthDateEmptyMessage()
      ├── IsValidDate()          → ShowDateInvalidMessage()
      ├── IsValidBirthDate()     → ShowBirthDateInvalidMessage()
      └── (valid) → CalculateAndFillResults()
                    ├── CalculateResults()
                    └── FillResults()
                         ├── FillCalculatedAgeResults()
                         ├── FillTimeLivedResults()
                         └── FillMoreDetailsResults()
                              └── CalculateAndFillNextBirthDay()

btnClear_Click
 ├── ClearAllTextboxes()
 │    ├── ClearBirthDateTextboxes()
 │    ├── ClearCalculatedAgeTextboxes()
 │    ├── ClearTimeLivedTextboxes()
 │    └── ClearMoreDetailsTextboxes()
 └── ReturnFocusToDayTextbox()
```

---

## ⚙️ Core Functionalities

| Section | Output |
|---|---|
| **Your Age Is** | Years · Months · Days + full summary sentence |
| **Time Lived** | Total Months · Weeks · Days · Hours · Minutes · Seconds |
| **More Details** | Day of week born · Days until next birthday |

---

## 🧠 Design Decisions Worth Noting

### Three-Layer Validation

The date goes through three independent checks, each with its own specific error message:

```csharp
// Layer 1 — is anything empty?
if (IsBirthDateEmpty()) → "Birth Date Should not be Empty!"

// Layer 2 — can it be parsed as a date?
if (!IsValidDate()) → "Date is Not Valid!"

// Layer 3 — is it a real birthdate?
if (!IsValidBirthDate()) → "\"Birth\" Date is Not Valid!"
```

The third check enforces that the date is in the past and no earlier than 1900.

Each error also calls `ClearBirthDateTextboxes()` and `ReturnFocusToDayTextbox()` — so the form resets to a clean state and the cursor goes right back to Day, ready for re-entry.

---

### ClearResults vs ClearAllTextboxes

Two distinct clear strategies:

```csharp
private void ClearResults()         // clears only output TextBoxes
private void ClearAllTextboxes()    // clears everything including inputs
```

`ClearResults()` is called at the START of every calculation.

This ensures leftover values from a previous result never mix with a new one — even if validation fails mid-way.

`ClearAllTextboxes()` is only used by the Clear button, which resets the entire form.

---

### Precise Total Months Calculation

Total months is not approximated from days.

It is calculated directly from the exact age:

```csharp
TotalMonths = (CalculatedAge_Years * 12) + CalculatedAge_Months;
```

The result is always accurate regardless of month length or leap years.

---

### Next Birthday Logic with Year Rollover

The next birthday is calculated with a guard for the case where the birthday has already passed this year:

```csharp
DateTime NextBirthDate = new DateTime(DateTime.Now.Year, BirthDate.Month, BirthDate.Day);

if (DateTime.Now >= NextBirthDate)
{
    NextBirthDate = NextBirthDate.AddYears(1);
}

txtNextBirthDay.Text = (NextBirthDate - DateTime.Now).Days.ToString();
```

If today is after the birthday this year, it moves to next year.

Otherwise it stays in the current year.

---

### Focus Management

Every error path ends with:

```csharp
private void ReturnFocusToDayTextbox()
{
    txtBirthDate_Day.Focus();
}
```

After any failed validation, the cursor goes straight back to Day.

No extra click needed. The user can just type the correct date immediately.

---

## 🛠️ Tech Stack

| | |
|---|---|
| **Language** | C# |
| **Framework** | .NET Framework |
| **UI** | Windows Forms (WinForms) |
| **IDE** | Visual Studio |
| **Type** | Desktop Application |
| **Controls Used** | Form, Label, TextBox, Button |

---
