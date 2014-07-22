UpVer
=====

Utility to increase version in UpppercuT config.

## Usage

Increase major: `upver --maj` or `upver --major`
Increase minor: `upver --min` or `upver --minor`
Increse patch: `upver --p` or `upver --patch`

If the location of `uppercut.config` is not set in the `app.config` then add a file argument:

`upver --maj -f '../settings/uppercut.config'`