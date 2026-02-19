# Programming Principles

This document describes the programming principles implemented in this project, based strictly on the current source code structure.

---

## 1) DRY (Don’t Repeat Yourself)

Repeated logic is centralized into reusable methods instead of duplicated.

### Implemented Examples

* Product formatting is implemented once and reused.
* Sorting logic is implemented via comparators and reused through `Array.Sort`.
* Utility UI flow logic is reused across menu branches.

### Code references

* [PrintProduct](./lab%204%20oop/Program.cs#L207-L218)
* [PrintProducts](./lab%204%20oop/Program.cs#L220-L229)
* [CompareByPrice](./lab%204%20oop/Program.cs#L241-L246)
* [CompareByQuantity](./lab%204%20oop/Program.cs#L248-L253)
* [SortProductsByPrice](./lab%204%20oop/Program.cs#L255-L256)
* [SortProductsByQuantity](./lab%204%20oop/Program.cs#L258-L259)
* [Pause](./lab%204%20oop/Program.cs#L140-L144)
* [EnsureProductsLoaded](./lab%204%20oop/Program.cs#L146-L157)

---

## 2) Encapsulation of domain calculations

All product calculations are encapsulated inside the `Product` structure.

### Implemented Calculations

* Unit price → `GetUnitPriceUAH()`
* Total price → `GetTotalPriceUAH()`
* Total weight → `GetTotalWeight()`

### Code references

* [Product struct + calculation methods](./lab%204%20oop/Program.cs#L17-L45)

---

## 3) Separation of responsibilities in program flow

Menu rendering, input handling, and processing logic are separated.

### Implementation

* `ShowMenu()` → UI rendering
* `HandleMenuChoice()` → command handling
* `ReadProductsArray()` → data input
* Output handled separately

### Code references

* [Main loop](./lab%204%20oop/Program.cs#L47-L63)
* [ShowMenu](./lab%204%20oop/Program.cs#L67-L98)
* [HandleMenuChoice](./lab%204%20oop/Program.cs#L100-L138)
* [ReadProductsArray](./lab%204%20oop/Program.cs#L159-L205)

---

## 4) Defensive input validation

User input is validated before processing.

### Implementation

* Numeric input uses `TryParse` loops
* Array usage is guarded by a loader check

### Code references

* [ReadProductsArray validation loops](./lab%204%20oop/Program.cs#L159-L205)
* [EnsureProductsLoaded](./lab%204%20oop/Program.cs#L146-L157)

---

