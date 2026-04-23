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
| Allure.NUnit | 2.14.0 |

---

## Project Structure

```
SdetPractice/
├── Base/
│   ├── BasePage.cs          # Abstract base for all page objects
│   ├── BaseTest.cs          # NUnit setup/teardown, driver lifecycle, Allure integration
│   └── IPage.cs             # Page contract interface
├── Configuration/
│   └── TestSettings.cs      # Typed config bound from appsettings.json
├── Drivers/
│   └── DriverFactory.cs     # WebDriver instantiation (Chrome, Firefox, Edge)
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
allureConfig.json            # Allure results output path configuration
appsettings.json             # Runtime test configuration (excluded from source control)
appsettings.example.json     # Configuration template
```

---

## Key Design Patterns

- **Page Object Model (POM)** — each page encapsulates its own locators and interactions
- **BasePage** — shared Selenium helpers (`Click`, `Type`, `IsVisible`, `GetRelativeNodeText`) inherited by all pages
- **BaseTest** — NUnit `[SetUp]`/`[TearDown]` manages driver lifecycle per test
- **Data-driven tests** — `[TestCaseSource]` with a dedicated `TestData` class as single source of truth
- **UI vs API separation** — 401 unauthorized scenarios tested via `HttpClient` to avoid Chrome's native auth popup limitation
- **Typed configuration** — `appsettings.json` bound to `TestSettings` for base URL, credentials, and timeouts
- **Allure reporting** — `[AllureSuite]` and `[AllureFeature]` attributes on every fixture; `[AllureNUnit]` activated globally via `BaseTest`

---

## Configuration

Create `appsettings.json` in the project root (copy from `appsettings.example.json`):

```json
{
  "TestSettings": {
    "BaseUrl": "https://the-internet.herokuapp.com",
    "Browser": "Chrome",
    "Headless": true,
    "ExplicitWaitSeconds": 10,
    "PageLoadTimeoutSeconds": 30,
    "BasicAuthUsername": "admin",
    "BasicAuthPassword": "admin"
  }
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

## Allure Reports

Requires the [Allure CLI](https://allurereport.org/docs/install/) (install via `scoop install allure`).

After running tests, generate and open the report:

```bash
allure generate allure-results --clean -o allure-report
allure open allure-report
```

> Results are written to `allure-results/` at the solution root. The report groups tests by **Suite** (UI Tests / API Tests) and **Feature** (per page).

---

## CI/CD

This project uses **GitHub Actions** for continuous integration. Every push or pull request to `main` automatically:

1. Restores NuGet packages and builds the project
2. Runs all tests (headless Chrome on `ubuntu-latest`)
3. Generates an Allure report
4. Uploads `allure-results` and `allure-report` as downloadable artifacts from the Actions run page

Workflow file: `.github/workflows/ci.yml`

---

## Test Coverage

| Page | Test IDs | Count | Type |
|---|---|---|---|
| AB Testing | TC001 | 1 | UI |
| Add/Remove Elements | TC002–TC005 | 4 | UI |
| Basic Auth | TC006 | 1 | UI |
| Basic Auth | TC011–TC012 + data-driven | 3 | API |
| Broken Images | TC016–TC020 | 5 | UI |
| Challenging DOM | TC021–TC028 | 8 | UI |
| Checkboxes | TC029–TC034 | 6 | UI |
| **Total** | | **28** | |
