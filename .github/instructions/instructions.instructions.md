---
applyTo: '**/*.cs'
---
# Top level design considerations
- never modify .csproj file directly, always use `dotnet` CLI



# class design considerations
- prefer imutable design
- prefer record types over imutable classes
- do not check for null in non-nullable reference types
-  prefer guard clasuses for argument validation
- prefer expression bodied members for simple properties and methods