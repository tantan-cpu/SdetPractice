# SdetPractice — Selenium Test Automation Framework

A C# Selenium test automation portfolio project built against [The Internet](https://the-internet.herokuapp.com/), demonstrating enterprise-level SDET practices.

---

## Tech Stack

| Tool | Version |
|---|---|
| .NET | 10.0 |
| C# | Latest |
| NUnit | 4.5.1 |
| Selenium WebDriver | 4.43.0 |
| FluentAssertions | 7.2.2 |
| Serilog | 4.3.1 |
| ChromeDriver | 147.x |

---

## Project Structure

```
SdetPractice/
├── Base/
│   ├── BasePage.cs          # Abstract base for all page objects
│   ├── BaseTest.cs          # NUnit setup/teardown, driver lifecycle
│   └── IPage.cs             # Page contract interface
├── Configuration/
│   └── TestSettings.cs      # Typed config bound from appsettings.json
├── Drivers/
│   └── DriverFactory.cs     # WebDriver instantiation
├── Pages/                   # Page Object Model classes
│   ├── ABTestingPage.cs
│   ├── AddOrRemoveElementsPage.cs
│   ├── BasicAuthPage.cs
│   ├── BrokenImagesPage.cs
│   ├── ChallengingDomPage.cs
│   └── CheckboxesPage.cs
├── Tests/
│   ├── API/
│   │   └── BasicAuthApiTests.cs
│   ├── UI/
│   │   ├── ABTestingUITests.cs
│   │   ├── AddOrRemoveElementsUITests.cs
│   │   ├── BasicAuthUITests.cs
│   │   ├── BrokenImagesUITests.cs
│   │   ├── ChallengingDomUITests.cs
│   │   └── CheckboxesUITests.cs
│   └── TestData/
│       └── BasicAuthTestData.cs
└── Utilities/
    ├── BasicAuthClient.cs   # HttpClient wrapper for API-level auth testing
    ├── ScreenshotHelper.cs
    ├── TestLogger.cs
    └── WaitHelper.cs
```

---

## Key Design Patterns

- **Page Object Model (POM)** — each page encapsulates its own locators and interactions
- **BasePage** — shared Selenium helpers (`Click`, `Type`, `IsVisible`, `GetRelativeNodeText`) inherited by all pages
- **BaseTest** — NUnit `[SetUp]`/`[TearDown]` manages driver lifecycle per test
- **Data-driven tests** — `[TestCaseSource]` with a dedicated `TestData` class as single source of truth
- **UI vs API separation** — 401 unauthorized scenarios tested via `HttpClient` to avoid Chrome's native auth popup limitation
- **Typed configuration** — `appsettings.json` bound to `TestSettings` for base URL, credentials, and timeouts

---

## Configuration

Create or update `appsettings.json` in the project root:

```json
{
  "BaseUrl": "https://the-internet.herokuapp.com",
  "ExplicitWaitSeconds": 10,
  "BasicAuthUsername": "admin",
  "BasicAuthPassword": "admin"
}
```

> `appsettings.json` is excluded from source control. Copy the structure above and set your own values.

---

## Running the Tests

```bash
dotnet test
```

Run a specific test class:

```bash
dotnet test --filter "FullyQualifiedName~CheckboxesUITests"
```

Run only UI or API tests:

```bash
dotnet test --filter "FullyQualifiedName~Tests.UI"
dotnet test --filter "FullyQualifiedName~Tests.API"
```

---

## Test Coverage

| Page | Test IDs | Type |
|---|---|---|
| AB Testing | TC001–TC003 | UI |
| Add/Remove Elements | TC004–TC005 | UI |
| Basic Auth | TC006 | UI |
| Basic Auth | TC007–TC010 | API |
| Broken Images | TC011–TC015 | UI |
| Challenging DOM | TC016–TC028 | UI |
| Checkboxes | TC029–TC034 | UI |
