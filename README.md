# Net.Glow.Studios.Common

This repository contains a collection of NuGet packages designed to extend the functionality of ASP.NET Core applications. Each package serves a specific purpose and provides various tools and utilities to streamline development tasks.

## Packages Overview

### Ngs.Common.AspNetCore

This package provides base types for common application elements such as entities, DTOs, and exceptions.

### Ngs.Common.AspNetCore.AccessControl

Enables access control and privilege management for actions and controllers within your ASP.NET Core application.

- **Usage Example**: https://github.com/NetGlow-Studios/Net.Glow.Studios.Common/tree/main/Examples/Ngs.Common.AspNetCore.AccessControl.Example

### Ngs.Common.AspNetCore.DataSower

Facilitates seeding of data into the database upon application startup.

- **Usage Example**: https://github.com/NetGlow-Studios/Net.Glow.Studios.Common/tree/main/Examples/Ngs.Common.AspNetCore.DataSower.Example

### Ngs.Common.AspNetCore.FluentFlow

Simplifies communication between the client-side and server-side by facilitating the exchange of information through requests to actions. It includes features for handling errors, redirects, validation, modals, etc.

### Ngs.Common.AspNetCore.Infrastructure

Offers extensive tools for creating database infrastructure, including entity configuration, repositories (including readonly and asynchronous), and support for MS SQL and SQLite.

- **Usage Example**: https://github.com/NetGlow-Studios/Net.Glow.Studios.Common/tree/main/Examples/Ngs.Common.AspNetCore.Infrastructure.Example

### Ngs.Common.AspNetCore.Notify

Supports email and SMS notifications, including an advanced email builder for constructing HTML email messages easily.

### Ngs.Common.AspNetCore.Storage

Manages file structures and their relationships with database references. Includes tools for file and directory management, backups, etc.

### Ngs.Common.AspNetCore.Tools

Contains utility tools utilized by other packages, such as extension methods to simplify common tasks.
