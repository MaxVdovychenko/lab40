# Programming Principles

This document describes the programming principles followed in this project, with references to the relevant code fragments.

---

## 1. DRY (Don't Repeat Yourself)

Repeated logic is extracted into separate reusable methods instead of being duplicated.

Examples include pause handling, product validation, and product printing.

Code references:  
- [Pause method](./lab%204%20oop/Program.cs#L133-L137)  
- [EnsureProductsLoaded method](./lab%204%20oop/Program.cs#L139-L150)  
- [PrintProduct / PrintProducts](./lab%204%20oop/Program.cs#L212-L220)  

---

## 2. Decomposition of functionality

Program logic is divided into small focused methods responsible for specific operations such as input, output, sorting, and analysis.

This improves readability and maintainability.

Code references:  
- [ReadProductsArray](./lab%204%20oop/Program.cs#L151-L199)  
- [GetProductsInfo](./lab%204%20oop/Program.cs#L222-L231)  
- [SortProductsByPrice / SortProductsByQuantity](./lab%204%20oop/Program.cs#L247-L250)  

---

## 3. Encapsulation of calculations

All price and weight calculations are implemented inside the `Product` structure, keeping business logic close to the data model.

Code references:  
- [GetUnitPriceUAH](./lab%204%20oop/Program.cs#L34-L37)  
- [GetTotalPriceUAH](./lab%204%20oop/Program.cs#L39-L42)  
- [GetTotalWeight](./lab%204%20oop/Program.cs#L44-L47)  

---

## 4. Defensive programming

User input is validated using parsing checks, and program operations verify that product data is loaded before processing.

This prevents runtime errors and invalid states.

Code references:  
- [Input validation in ReadProductsArray](./lab%204%20oop/Program.cs#L151-L198)  
- [EnsureProductsLoaded](./lab%204%20oop/Program.cs#L139-L150)  

---

## Notes on possible improvements

The `Main` method handles menu rendering, input handling, and control flow, which partially violates the Single Responsibility Principle and could be further decomposed in future refactoring.

Code reference:  
- [Main method](./lab%204%20oop/Program.cs#L52-L255)
