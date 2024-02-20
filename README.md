# Flobird API - Dokumentacja projektu

## Opis projektu

Flobird API to zaawansowana aplikacja webowa typu Kanban, zbudowana w oparciu o nowoczesny stack technologiczny .NET. System umożliwia kompleksowe zarządzanie projektami, zadaniami i zespołami poprzez RESTful API z pełną autoryzacją JWT.

---

## 🚀 Główne funkcjonalności

### Zarządzanie projektami (Boards)

- Tworzenie, edycja i usuwanie tablic projektów
- Przypisywanie użytkowników z różnymi rolami (Creator, Admin, User)
- Zaawansowane zarządzanie uprawnieniami na poziomie tablicy

### System list i kart (Lists & Cards)

- Organizacja zadań w listach z możliwością ustalania pozycji
- Pełny CRUD dla kart z zaawansowanymi metadanymi (deadline, opisy)
- Przenoszenie kart między listami

### Szczegółowe zarządzanie zadaniami

- Hierarchia: `Board → List → Card → Task → Element`
- Przypisywanie użytkowników do poszczególnych elementów
- System komentarzy i załączników

---

## 🔐 Bezpieczeństwo i autoryzacja

- Uwierzytelnianie JWT z rolami użytkowników
- Zaawansowany system uprawnień oparty na zasobach
- Autoryzacja na poziomie poszczególnych operacji (CRUD)

---

## 🛠 Stack technologiczny

### Backend

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core 8
- AutoMapper
- FluentValidation

### Bezpieczeństwo

- JWT Bearer Authentication
- ASP.NET Core Identity
- Role-based Authorization
- Password Hashing

### Dokumentacja i DevOps

- Swagger/OpenAPI
- Azure Blob Storage
- CORS

### Wzorce architektoniczne

- Repository Pattern
- Dependency Injection
- Service Layer
- DTO Pattern
- Middleware

---

## 📋 Struktura projektu

### Warstwy aplikacji

- **API** - Kontrolery, middleware, konfiguracja
- **Application** - Logika biznesowa, serwisy, DTO, walidacja
- **Domain** - Modele domenowe, repozytoria, wyjątki
- **Infrastructure** - Implementacja EF Core, migracje, kontekst bazy

### Kluczowe modele domenowe

- **User** - Użytkownicy systemu z avatarami
- **Board** - Tablice projektów z listami
- **List** - Listy z kartami
- **Card** - Karty z zadaniami i załącznikami
- **Task** - Zadania z elementami
- **Element** - Elementy zadań (checklisty)

---

## 🔐 System autoryzacji

### Role użytkowników

- **Creator** - Pełne uprawnienia (w tym usuwanie boardów)
- **Admin** - Zarządzanie zawartością (bez usuwania boardów)
- **User** - Podstawowe operacje (komentarze, przypisania)

### Operacje na zasobach

```csharp
public enum ResourceOperations
{
    Create,
    Read,
    Update,
    Delete
}
## 📊 Endpointy API

### Authentication (`/accounts`)
- `POST /register` - Rejestracja nowego użytkownika
- `POST /login` - Logowanie z zwrotem tokenu JWT

### Users (`/users`)
- `GET /users` - Pobieranie danych użytkownika
- `PUT /users` - Aktualizacja profilu
- `GET /users/all` - Wyszukiwanie użytkowników

### Boards (`/boards`)
- `POST /boards` - Tworzenie nowej tablicy
- `GET /boards` - Lista tablic użytkownika
- `PUT /boards` - Aktualizacja tablicy
- `POST /members/boards/{id}` - Dodawanie użytkowników

### Lists (`/lists`)
- `POST /lists` - Tworzenie listy
- `GET /lists/boards` - Pobieranie list dla boardu
- `PATCH /lists` - Ustawianie deadline'u

### Cards (`/cards`)
- `POST /cards/lists` - Dodawanie karty do listy
- `GET /cards/lists` - Karty w liście
- `PUT /cards` - Aktualizacja karty

---

## 🗄 Baza danych
- SQL Server z Entity Framework
- Zaawansowane relacje wiele-do-wielu
- Kaskadowe usuwanie z zachowaniem integralności
- Indeksowanie kluczowych pól
- Walidacja na poziomie bazy danych

### Migracje
- Automatyczne aplikowanie migracji przy starcie
- Version-controlled schema changes

---

## 🔧 Konfiguracja i uruchomienie

### Wymagania
- .NET 8.0 SDK
- SQL Server (local lub Azure)
- Azure Storage Account (dla plików)

### Zmienne środowiskowe
{
  "ConnectionStrings": {
    "LocalDbConnection": "...",
    "AzureDbConnection": "..."
  },
  "Authentication": {
    "JwtKey": "...",
    "JwtExpireTime": 60,
    "JwtIssuer": "...",
    "JwtAudience": "..."
  },
  "Azure": {
    "AzureStorageConnection": "...",
    "ContainerName": "..."
  }
}
## 🎯 Kluczowe cechy jakościowe

### Bezpieczeństwo
- Hashowanie haseł z salt
- Token JWT z ograniczonym czasem życia
- Walidacja danych wejściowych
- Ochrona przed SQL Injection

### Wydajność
- Async/await w całej aplikacji
- Eager loading związanych danych
- Optymalne zapytania do bazy

### Obsługa błędów
- Global exception handling middleware
- Strukturyzowane kody odpowiedzi
- Logowanie błędów

### Testowalność
- Dependency Injection
- Separacja concerns
- Czysta architektura

---

## 📈 Rozwój projektu

### Możliwości rozbudowy
- System powiadomień w czasie rzeczywistym
- Zaawansowane raportowanie
- Integracja z narzędziami zewnętrznymi
- Aplikacja mobilna

---

## Autor
- **Jakub Ros**
- Kontakt: jakub.rosploch@gmail.com
- GitHub: [https://github.com/JakubRoss](https://github.com/JakubRoss)
```
