# Wolters Kluwer L1 Interview FAQ (with examples)

Use this as a quick revision sheet for an L1 round. Keep answers short (45-90 seconds) and practical.

## 1) Tell me about yourself.

**Sample answer**
I am a full-stack developer with hands-on experience in C#/.NET for backend APIs and Angular/TypeScript for frontend applications. I have worked with SQL and NoSQL databases, built REST APIs, and followed Git-based workflows with pull requests and code reviews. I am comfortable with unit testing, dependency injection, and clean architecture. I also use AI-assisted tools like Copilot/Cursor to speed up boilerplate, refactoring, and test generation while keeping quality checks through review and tests.

---

## 2) What is the difference between .NET Framework and .NET (Core/5+)?

**Sample answer**
- .NET Framework is Windows-focused and legacy for older enterprise apps.
- Modern .NET (Core/5/6/7/8) is cross-platform, faster, and container/cloud friendly.
- For new projects, I prefer .NET 8 with ASP.NET Core.

**Example**
Deploying an ASP.NET Core API in Linux containers on Azure App Service is straightforward with modern .NET.

---

## 3) Explain dependency injection (DI) in .NET.

**Sample answer**
DI means objects receive dependencies from outside instead of creating them internally. It improves testability, flexibility, and maintainability.

**Example (Program.cs)**
```csharp
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
```
Now `InvoiceController` can receive `IInvoiceService` via constructor injection.

---

## 4) What OOP concepts do you use in daily work?

**Sample answer**
I use encapsulation, inheritance, polymorphism, and abstraction. In enterprise code, I rely heavily on abstraction and composition with interfaces to keep services loosely coupled.

**Example**
`IPaymentGateway` interface with implementations like `StripeGateway` and `MockGateway` for testing.

---

## 5) What data structures are commonly used in C# projects?

**Sample answer**
- `List<T>` for ordered collections.
- `Dictionary<TKey, TValue>` for fast lookup by key.
- `HashSet<T>` for uniqueness checks.
- `Queue<T>` for FIFO workflows (background tasks).

**Example**
Use `Dictionary<string, Patient>` for quick lookup by patient ID.

---

## 6) How do you handle multithreading in .NET?

**Sample answer**
I prefer async/await for I/O-bound operations and `Task.WhenAll` for parallel calls. For CPU-bound tasks, I use `Task.Run` carefully. I avoid shared mutable state and use thread-safe patterns.

**Example**
```csharp
var task1 = _labApi.GetResultsAsync(patientId);
var task2 = _billingApi.GetInvoicesAsync(patientId);
await Task.WhenAll(task1, task2);
```

---

## 7) What is REST API and how do you design one?

**Sample answer**
REST API exposes resources via HTTP methods (`GET`, `POST`, `PUT`, `DELETE`) with clear status codes and predictable routes. I design endpoints around nouns, add validation, and return standard error responses.

**Example**
- `GET /api/patients/{id}`
- `POST /api/appointments`
- Return `201 Created` with location header for successful creation.

---

## 8) Difference between SQL and NoSQL? When do you use each?

**Sample answer**
- SQL is relational, schema-based, and strong for complex joins and transactions.
- NoSQL is flexible schema and useful for high-scale or document-driven use cases.
- In healthcare/finance-like systems, core transactional data usually stays in SQL.

**Example**
- SQL: billing, claims, patient master data.
- NoSQL: audit events, activity logs, session documents.

---

## 9) How do you optimize SQL query performance?

**Sample answer**
I check execution plans, add proper indexes, avoid `SELECT *`, and paginate large datasets. I also review slow queries and tune joins/filters.

**Example**
```sql
SELECT PatientId, FullName
FROM Patients
WHERE LastName = @LastName
ORDER BY PatientId
OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;
```

---

## 10) What is your Angular architecture approach?

**Sample answer**
I use module/feature-based structure, smart and presentational components, reusable services, route guards, and interceptors for auth/error handling.

**Example**
- `patient-list.component` for UI
- `patient.service.ts` for API calls
- interceptor for JWT token and global error handling

---

## 11) Why TypeScript over JavaScript in enterprise apps?

**Sample answer**
TypeScript provides static typing, better tooling, safer refactoring, and fewer runtime errors. This is valuable in large codebases and teams.

**Example**
A typed API response interface prevents accidental access to missing properties.

---

## 12) Have you worked with Kendo UI?

**Sample answer**
Yes, I have used component libraries like Kendo UI for enterprise-ready grids, forms, and charts. Key areas are server-side paging/sorting, validation, and consistent UX.

**Example**
Kendo Grid with server-side pagination for large claims dataset.

---

## 13) How do you secure secrets in Azure?

**Sample answer**
I store secrets in Azure Key Vault, avoid hardcoding credentials, and access them via managed identity. I also rotate secrets and limit access with RBAC.

**Example**
Connection strings and API keys are fetched from Key Vault at startup.

---

## 14) Which Azure services have you used?

**Sample answer**
I have worked with Azure App Service/Web Apps for deployment, Azure SQL for relational data, Storage Accounts for files/queues, Key Vault for secrets, and App Insights for logging/monitoring. I am familiar with CI/CD pipelines.

---

## 15) How do you monitor and troubleshoot production issues?

**Sample answer**
I use structured logging, correlation IDs, and application metrics. In Azure, App Insights helps track failed requests, exceptions, dependency latency, and custom events.

**Example**
Investigate 500 errors by filtering operation ID and tracing API -> DB dependency calls.

---

## 16) What is your Git workflow?

**Sample answer**
I use feature branches, small commits, pull requests, and code reviews before merge. I keep commit messages descriptive and sync regularly with main.

**Example**
1. Create branch: `feature/patient-search`
2. Commit in small logical units
3. Open PR with testing notes
4. Address review comments and merge

---

## 17) Explain pull request best practices.

**Sample answer**
A good PR has a clear title, summary of change, screenshots (if UI), test evidence, and risk notes. I keep PR size manageable and avoid mixing unrelated changes.

---

## 18) Which unit test frameworks have you used?

**Sample answer**
For .NET I use xUnit/NUnit with mocking libraries like Moq. For Angular I use Jasmine/Karma (or Jest depending on project). I focus on business logic and edge cases.

**Example (xUnit)**
```csharp
[Fact]
public void CalculateTotal_ReturnsSum()
{
    var service = new InvoiceService();
    Assert.Equal(300, service.CalculateTotal(100, 200));
}
```

---

## 19) What is your approach to API testing?

**Sample answer**
I do unit tests for service logic, integration tests for controller + DB behavior, and use Postman/Swagger for quick endpoint validation. I verify status codes, payloads, and error scenarios.

---

## 20) What is Service Oriented Architecture (SOA) in simple terms?

**Sample answer**
SOA means building systems as independent services that communicate through well-defined contracts/APIs. It improves reusability and independent deployment.

**Example**
Patient service, billing service, and notification service interacting via APIs/events.

---

## 21) How do you use AI-assisted development tools responsibly?

**Sample answer**
I use AI tools for boilerplate generation, refactoring suggestions, test case ideas, and documentation drafts. I always validate output with code review, static analysis, security checks, and tests before merging.

**Example workflow**
1. Ask AI to draft unit tests
2. Manually review edge cases
3. Run tests and linting
4. Commit only verified code

---

## 22) Linux/scripting experience - what can you mention in L1?

**Sample answer**
I am comfortable with basic Linux commands, log checks, process monitoring, and shell scripts for automation.

**Example**
Simple deployment health check script:
```bash
#!/usr/bin/env bash
set -e
curl -f https://example-api/health
echo "API is healthy"
```

---

## 23) What is Agile in your day-to-day work?

**Sample answer**
I work in sprints, estimate tasks, attend standups, and deliver incremental value. I collaborate closely with QA/product, and adapt based on feedback during sprint reviews/retrospectives.

---

## 24) Have you prepared design documentation?

**Sample answer**
Yes, I create high-level design (architecture, modules, integrations) and low-level design (class flow, DB schema, API contract, validations). Good documentation reduces onboarding and defects.

---

## 25) Healthcare domain experience - how to answer if limited?

**Sample answer**
I may not have deep healthcare domain exposure yet, but I understand the importance of data privacy, auditability, compliance, and reliability. I can ramp up quickly and align implementation with domain and regulatory requirements.

---

## Rapid-fire technical answers (one-liners)

- **`IEnumerable` vs `IQueryable`**: `IEnumerable` works in-memory, `IQueryable` translates to DB query.
- **`var`, `let`, `const` in JS/TS**: prefer `const`, use `let` for reassignment, avoid `var`.
- **HTTP 401 vs 403**: 401 unauthenticated, 403 authenticated but not authorized.
- **`async`/`await` benefit**: non-blocking I/O and better scalability.
- **Why indexes matter**: faster reads at the cost of some write overhead and storage.

---

## Mini mock questions with crisp responses

### Q: How would you build a "Patient Search" feature end-to-end?
**A:** Angular UI with debounced search input, .NET API endpoint with pagination/filtering, SQL indexed search columns, unit tests for service logic, API monitoring via App Insights, and secure config via Key Vault.

### Q: How do you avoid breaking changes in API integration?
**A:** Version APIs, keep backward compatibility, add contract tests, and communicate deprecations clearly.

### Q: What would you do if AI-generated code looks correct but you are unsure?
**A:** Validate with tests, static analysis, security checks, and peer review before merge.

---

## Final L1 tips

1. Keep answers structured: **Concept -> Real example -> Result**.
2. For each skill in JD, prepare at least one project example.
3. If you do not know something, say:  
   "I have not used it deeply yet, but I understand the fundamentals and can learn quickly."
4. Show strong engineering habits: testing, code reviews, documentation, and clean Git workflow.

Good luck - you are covering the right stack for this role.
