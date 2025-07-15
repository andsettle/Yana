# Yana Development Roadmap: From Concept to Portfolio Showcase

This document outlines the planned development phases and key tasks for "Yana" (Your Awesome Notes App). The goal is to build a robust C# application across API, web, and native platforms, serving as a comprehensive portfolio piece.

---

## Current Status:

* **Yana.Api:** Functional RESTful API with full CRUD (Create, Read, Update, Delete) operations for `Note` objects, currently using **in-memory data**.
* **Yana.Client:** Unified .NET MAUI Blazor Web App project, capable of running as a Windows Desktop app, Android app, and a browser-based web app. `HttpClient`, `INotesService`, and `NotesService` (shared) are configured and ready for API communication.
* **Yana.Client.Shared:** Contains common code, including the `Note` model, `INotesService` interface, and the shared `NotesService` implementation.
* **Yana.Client.Web:** Dedicated Blazor web app project, configured to consume services from `Yana.Client.Shared` and the API.
* **Git Repository:** Project is under version control on GitHub with a `.gitignore` configured.

---

## Phase 1: Core Client UI/UX & API Integration (Current Focus: Windows/Android/Web)

**Goal:** Get the `Yana.Client` (both MAUI and Web aspects) fully functional with CRUD operations by consuming the `Yana.Api` (still using in-memory data). This phase will solidify your Blazor and API consumption skills.

* ~~**Task 1.1: Display All Notes (GET Integration)**~~
    ~~* Create `NotesList.razor` in `Yana.Client.Shared/Components/Pages`.~~
    ~~* Inject `INotesService`.~~
    ~~* Call `GetNotesAsync()` on component initialization.~~
    ~~* Display the retrieved notes in a user-friendly list format (e.g., table, cards).~~
* ~~**Task 1.2: Add New Note Form (POST Integration)**~~
    ~~* Create a Blazor form component for creating a new note.~~
    ~~* Bind form inputs to a new `Note` object (or a DTO).~~
    ~~* Call `INotesService.CreateNoteAsync()` (will add this to the service).~~
    ~~* Handle success/error messages in the UI.~~
* **Task 1.3: View Note Details (GET by ID Integration)**
    * Create a Blazor page/component to display a single note's details.
    * Implement navigation from the list to the detail view (e.g., using `NavLink` or `NavigationManager`).
    * Call `INotesService.GetNoteByIdAsync()` (will add this to the service).
* **Task 1.4: Edit Existing Note Form (PUT Integration)**
    * Adapt the new note form or create a dedicated edit form.
    * Pre-populate the form with existing note data fetched via `GetNoteByIdAsync()`.
    * Call `INotesService.UpdateNoteAsync()` (will add this to the service).
    * Handle success/error, navigate back to list/details.
* **Task 1.5: Delete Note Functionality (DELETE Integration)**
    * Add a delete button/icon to note list items or detail view.
    * Implement a confirmation dialog.
    * Call `INotesService.DeleteNoteAsync()` (will add this to the service).
    * Update the UI after successful deletion.
* **Task 1.6: Basic UI/UX Polish:**
    * Ensure responsive layout for different screen sizes (Windows desktop, Android phone, web browser).
    * Implement basic loading indicators.
    * Provide user-friendly error messages in the UI.

---

## Phase 2: Data Persistence

**Goal:** Transition your `Yana.Api` from using in-memory data to a real database, ensuring that notes persist even after the API restarts.

* **Task 2.1: Choose a Database:** Start with **SQLite** for local development and testing (it's file-based, zero setup, great for development).
* **Task 2.2: Introduce Entity Framework Core (EF Core):**
    * Install EF Core NuGet packages in `Yana.Api`.
    * Create an `ApplicationDbContext` (your database context).
    * Configure EF Core in `Program.cs`.
    * Replace `_notes` static list with `DbContext` interaction in `NotesController`.
* **Task 2.3: Database Migrations:**
    * Learn and apply EF Core migrations to create and update your database schema.
* **Task 2.4: Seed Initial Data (Optional):**
    * Add some initial notes to your database programmatically for easy testing.
* **Task 2.5: Test API with Database:**
    * Thoroughly re-test all CRUD operations in Swagger UI after integrating the database.

---

## Phase 3: Enhanced Client Features & Polish

**Goal:** Make the `Yana.Client` more user-friendly and feature-rich, enhancing the overall user experience.

* **Task 3.1: Client-Side Input Validation:**
    * Implement Blazor's built-in form validation (e.g., `EditContext`, `DataAnnotations`) for your note forms.
* **Task 3.2: Search and Filter Notes:**
    * Add a search bar to the notes list.
    * Implement filtering logic in your `NotesService` and/or `Yana.Api` to display relevant notes.
* **Task 3.3: Richer Text Editor (Optional):**
    * Integrate a simple third-party rich text editor component for note content, allowing for formatting.
* **Task 3.4: State Management (Optional but good to know):**
    * Explore basic Blazor state management patterns to optimize UI updates and avoid excessive API calls.

---

## Phase 4: API Enhancement & Security

**Goal:** Secure your API with user authentication and authorization, and prepare it for real-world deployment in the cloud.

* **Task 4.1: User Accounts:**
    * Introduce `User` models and basic user management (registration, login) in `Yana.Api`.
* **Task 4.2: Authentication & Authorization:**
    * Implement JWT (JSON Web Tokens) authentication for your API.
    * Protect API endpoints with authorization policies (e.g., only logged-in users can access/modify *their* notes).
* **Task 4.3: Cloud Database Integration:**
    * Transition your API to use a cloud-hosted database (e.g., Azure SQL Database, PostgreSQL on Render.com's free tier) for scalable persistence.
* **Task 4.4: Deploy `Yana.Api` to Cloud:**
    * Deploy your `Yana.Api` to a cost-effective cloud service (e.g., Azure App Service Free Tier, Render.com). Update `HttpClient.BaseAddress` in client projects.

---

## Phase 5: Portfolio Integration & Advanced MAUI Features

**Goal:** Integrate Yana into your main developer portfolio site and explore advanced MAUI capabilities to further showcase your skills.

* **Task 5.1: Build/Enhance Portfolio Site:**
    * Create or refine your main Blazor portfolio site.
    * Add dynamic links or sections for Yana, potentially consuming its API for live data or statistics.
* **Task 5.2: MAUI Platform Integrations:**
    * Explore MAUI-specific features like local notifications, platform-specific file system access, or device information.
    * Consider implementing offline data synchronization for the MAUI client (a more advanced feature).
* **Task 5.3: Deployment to App Stores (Optional):**
    * Prepare your MAUI application for submission to platforms like the Microsoft Store (Windows), Google Play Store (Android), and Apple App Store (iOS - requires Mac setup).

---
