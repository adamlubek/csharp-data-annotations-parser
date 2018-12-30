# csharp-data-annotations-parser
Parsing of C# data annotations to TypeScript/Angular Validators

## Features
Parses C# data annotations to
- TypeScript constants
- Angular Validators which can be used in Reactive Forms

## Install

```
$ npm install csharp-data-annotations-parser
```


## Usage

```
$ parse-csharp-data-annotations --dll="C:\dllfolder\somedll.dll" --tsConstantsDestination="C:\temp\ts-constants.ts" --ngReactiveFormValidatorsDestination="C:\temp\ng-validators.ts"
```

Arguments
- --dll (path to c# dll)

Options
- --tsConstantsDestination (output path of TypeScript constants file)
- --ngReactiveFormValidatorsDestination (output path of Angular Validators file)