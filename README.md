A console application for importing to and exporting data from a database (SQL Server for developers https://www.microsoft.com/en-us/sql-server/developer-get-started/csharp/win/)

Format:
Data import from a file to a database:
Impexpdata -import

Imports all data from the provided file to the database (design the database)

Input file: a CSV file (with semicolon)
Format:
#Comment line - optional
;;;

Sample:
#Some text
234;Customer1;
4324;Customer2;Details

Data export from the database to a CSV file:
Impexpdata -export

Exports all data from the database that have never been exported yet to a file (one-time export)

Output file: a CSV file (with semicolon)
Format:
#Comment line - optional
;;;;

Sample:
4324;Customer2;Details;Special customer;2018-02-23 12:00:00; 2018-02-24 15:00:00

Customer data validation for the database:
Customer id: integer not null
Customer name: up 20 chars not null
Notes: up to 200 chars, nullable
Code: integer, nullable

The from the input data is an integer value (empty values are allowed). The for the output data is to be taken from a database table containing integer code ids (not nullable) and code names (up to 200 chars, not nullable), i.e., the name gets looked up by the provided code id. Please note the that the code ids provided with the input data may be empty (;;), existing ids (e.g., ;1; - corresponds to “Special customer” in the lookup table), or non-existing ids (e.g., ;404; - doesn’t correspond to any record in the code lookup table) The expected output data () would be:
;; ;;
;1; ;Special customer;
;404; ;!!! INVALID CODE !!!;

Requirement: use an OUTTER JOIN between the DB tables for selecting output data.

Provide logging (e.g., by log4net).
Provide fault-tolerance (file access error, database errors, data validation errors.)
Ensure that no valid data records are lost during the import/export operations.
In case of invalid customer data in the input file write the invalid data record to the log for future reference and continue with the next record.
The implementation details which are not mentioned, are up to the developer.
