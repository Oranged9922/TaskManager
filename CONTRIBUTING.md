# Contributing to TaskManager

## Table of Contents
- [Getting Started](#getting-started)
- [Coding Guidelines](#coding-guidelines)
- [Pull Requests](#pull-requests)
- [Issues](#issues)
- [Code of Conduct](#code-of-conduct)

## Getting Started
1. Fork the repository.
2. Clone your fork locally.
   ```bash
   git clone https://github.com/YOUR_USERNAME/TaskManager.git
   ```
3. Add the upstream repository.
   ```bash
   git remote add upstream https://github.com/Oranged9922/TaskManager.git
   ```
4. Create a new branch for your work.
   ```bash
   git checkout -b feature/new-feature
   ```
5. Install dependencies.
   ```bash
   dotnet restore
   ```

## Coding Guidelines
- Follow C# coding conventions.
- Use FluentValidation for input validation.
- Use MediatR for CQRS implementation.
- Write unit tests for new features.

## Pull Requests
1. Fetch the latest changes from upstream.
   ```bash
   git fetch upstream
   ```
2. Rebase your branch.
   ```bash
   git rebase upstream/master
   ```
3. Push your changes.
   ```bash
   git push origin feature/new-feature
   ```
4. Create a pull request targeting the `master` branch.

## Issues
- Use the issue templates.
- Be specific and provide code samples if possible.