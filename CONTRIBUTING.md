We follow the official [Microsoft coding conventions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions)
as a base for our codebase.

Abobe this conventions we have some rules to keep the code uniform:
- All methods that create something, should be called `Create` (not `add` or `new`)
- All methods that edit something, should be called `Edit` (not `update` or `change`)
- All methods that delete something, should be called `Delete` (not `remove` or `erase`)
- If a method reutrns a list, can include the `Find` keyword (examples: `FindClients`, `FindPendingTickets`)
- If a method reutrns a single objet, can include the `Get` keyword (examples: `GetClient(int id)`, `GetTicket(int id)`)
