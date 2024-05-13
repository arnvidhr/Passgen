# Passgen
Command line password generator, where possible to specify number of passwords, their length, symbols which would be used.

Usage: PASSGEN [number of passwords] [length] [used symbols]

Where used sybols can be:
 d - digits;
 u - upper case letters;
 l - lower case letters;
 s - symbols (ASCII ! to / and [ to `);

Eg. PASSGEN 10 120 duls would generate 10 passwords of 120 symbols, using all available characters: digits, upper case, lower case and symbols.
