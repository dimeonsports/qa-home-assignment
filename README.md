# QA Home Assignment

## 📌 Overview

This repository contains the home assignment for QA position at Riverty. 
The assignment involves writing unit tests and automated tests for a payment application that validates credit card data.

## 🧩 Functional Scope

The functinal scope of the assignment includes:
- Validating credit card data including card owner, credit card number, issue date, and CVC.
- Returning the type of credit card application (Master Card, Visa, or American Express) upon successful validation.
- Returning validation errors in case of failure.
- Writing unit tests that cover at least 80% of the application.
- Writing integration tests using the Reqnroll framework.
- (Optional) Creating a Docker pipeline to run unit and integration tests and produce test execution results.

## 🧪 Test Groups

The tests are organized into the following groups:
- Unit tests
- Integration tests

## 📊 Test & Coverage Reports (CI)

After a pipeline run:

1. Open **GitHub → Actions**
2. Select a workflow run
3. Download artifact:
   - `test-and-coverage-html`
4. Open `index.html` locally in a browser

The report contains:
- Test execution summary
- Passed / failed test details
- Code coverage overview

## 🐳 Running Tests with Docker

### Requirements:

- - Docker installed and running on your machine.

### Prerequisites

1. 1. Please install following:

- [Docker](https://www.docker.com/)

2. Check Docker:
```bash
docker --version
docker info
```
3. Run Docker


### Build Docker Image

Run from the repository root:
```bash
docker build -t qa-home-assignment
```

### Run Tests in Docker Container

Run tests inside Docker

```bash
docker run --rm qa-home-assignment
```

### View reports from Docker to your host machine

If your Dockerfile/script generates TestResults/ and reports/ inside the container, you can mount a local folder:

#### Linux / macOS (bash/zsh)

From the repository root:
```bash
mkdir -p artifacts
docker run --rm \
  -v "$(pwd)/artifacts:/artifacts" \
  qa-home-assignment
```

Then ensure your container copies results to /artifacts (common pattern):

/artifacts/TestResults
/artifacts/reports

After the run you will find on your host:

./artifacts/TestResults/
./artifacts/reports/

##### Alternative (macOS zsh) – explicit path

```zsh
mkdir -p artifacts
docker run --rm \
  -v "$PWD/artifacts:/artifacts" \
  qa-home-assignment
  ```

#### PowerShell (Windows):

```powershell
mkdir artifacts -Force | Out-Null
docker run --rm `
  -v "${PWD}\artifacts:/artifacts" `
  qa-home-assignment
```

Then ensure your container copies results to /artifacts (common pattern):

/artifacts/TestResults
/artifacts/reports

If you already generate results directly into /artifacts inside the container, after the run you will find:

.\artifacts\TestResults\
.\artifacts\reports\