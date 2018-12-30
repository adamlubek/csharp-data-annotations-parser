#!/usr/bin/env node
const exec = require('child_process').execFile;
const processArg = argName => {
  const arg = process.argv.find(e => e.startsWith(`--${argName}`));
  return arg ? arg.split('=')[1] : undefined;
}

exec(
  __dirname + `./assets/App.exe`,
  [
    processArg('dll'),
    processArg('tsConstantsDestination'),
    processArg('ngReactiveFormValidatorsDestination')
  ],
  err => {
    console.log(err ? err : 'ts file generated');
  }
);