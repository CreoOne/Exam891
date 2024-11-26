# Exam891

## Build

This project depends on .NET 8 SDK

In `./sources` directory:

```
dotnet build
```

## Run

Main executable is called `Exam891.Cli` and it will enter interactive mode, where application reads user input until end of line character is typed.

### Running from build artifacts

Currently the application can only be executed from build artifacts.

In order to start the app, please go to:

- `sources/Exam891.Cli/bin/Debug/net8.0`
- `sources/Exam891.Cli/bin/Release/net8.0`

_(Depends on build configuration)_

### Arguments

Executable requires arguments which can be used like this:

```
./Exam891.Cli.exe --bookings bookings.json --hotels hotels.json
```

#### Bookings (required)

Locates JSON file where bookings information is provided and then loads it into application memory. This file is not written to.

_Json file structure with accordance to provided documentation_

Example:

```
--bookings bookings.json
```

#### Hotels (required)

Locates JSON file where hotels information is provided and then loads it into application memory. This file is not written to.

_Json file structure with accordance to provided documentation_

Example:

```
--hotels hotels.json
```

### Commands

Commands can be typed in after application is correctly started.

**Commands and parameters are case sensitive**

#### Search

Search query allows to retrieve information about available rooms from now until specified range in days.

- `hotelId` (string) - identifies which hotel this search is related to
- `daysLimit` (int) - specifies how far in the future search needs to be performed (in days)
- `roomType` (string) - identifies which room type this search is related to

Example:

```javascript
Search(HotelName, 40, SGL)
```

Returns time range and amount of available rooms for this range.

Example:

```
(20241125-20241201, 2),
(20241201-20241203, 1),
(20241203-20251125, 2)
```

#### Availability

Availability query allows to retrieve information about number of available rooms for specific day or date range.

- `hotelId` (string) - identifies which hotel this search is related to
- `date` (date) - specifies the day for which the availability check needs to be performed (cannot be used with `dateRange` parameter)
- `dateRange` (date range) - specifies the start and end date range (separated with `-` character) for which the availability check needs to be performed (cannot be used with `date` parameter)
- `roomType` (string) - identifies which room type this search is related to

Examples:

```javascript
Availability(H1, 20241201, SGL)
Availability(H1, 20241201-20241203, DBL)
```

Returns number of available rooms for this range.

Example:

```
2
```

## Technical overview

This project was created with simplicity and extensibility in mind. Although it is not implemented yet, it is possible to embed almost all of its functionality inside Web App or more pipeline friendly version of CLI.

### Project structure

- `sources/Exam891.Cli/` - contains logic related to running this application from CLI, like parsing arguments, parsing commands, main control etc.
- `sources/Exam891.Core/` - contains logic related to algorithmic side of the application, like parsing json files, storing, retrieving and processing hotels and bookings information.
- `sources/Exam891.Cli.Tests` - contains tests related to `Exam891.Cli` project.
- `sources/Exam891.Core.Tests` - contains tests related to `Exam891.Core` project.

## Notes

- Used CoPilot for accelerating productivity
- Written using Visual Studio 2022 and Visual Studio Code
- Assumed all dates are written in UTC