/****************************************************************
* List tables in a database. I use this to build scripts and illustrations. 
*****************************************************************/
SELECT DISTINCT c1.table_name
FROM INFORMATION_SCHEMA.COLUMNS c1 
  INNER JOIN INFORMATION_SCHEMA.TABLES T ON t.TABLE_NAME = c1.TABLE_NAME
WHERE t.TABLE_TYPE = 'BASE TABLE' AND t.TABLE_NAME NOT LIKE 'sys%'


/****************************************************************
* Search for particular columns in a database. Useful for reverse 
* engineering a database with a million tables.
*****************************************************************/
SELECT c1.column_name, c1.table_name
FROM INFORMATION_SCHEMA.COLUMNS c1 
  INNER JOIN INFORMATION_SCHEMA.TABLES T ON t.TABLE_NAME = c1.TABLE_NAME
WHERE t.TABLE_TYPE = 'BASE TABLE' AND t.TABLE_NAME NOT LIKE 'sys%'
AND c1.COLUMN_NAME LIKE '%ChargeMessageID%'



/****************************************************************
* Undocumented stored procedures
* 1) Cycle through all tables displaying name, rows, reserved & data space, index size and unused space
* 2) Cycle through all databases displaying DB size, owner, created datetime, status and compatibility level
*****************************************************************/
EXEC sp_MSforeachtable @command1="EXEC sp_spaceused '?'"
EXEC sp_MSforeachdb @command1="EXEC sp_helpdb '?'"




/****************************************************************
* Relational Schema
*****************************************************************/
SELECT
K_Table = FK.TABLE_NAME,
FK_Column = CU.COLUMN_NAME,
PK_Table = PK.TABLE_NAME,
PK_Column = PT.COLUMN_NAME,
Constraint_Name = C.CONSTRAINT_NAME
FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C
INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME
INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME
INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME
INNER JOIN (
SELECT i1.TABLE_NAME, i2.COLUMN_NAME
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1
INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2 ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME
WHERE i1.CONSTRAINT_TYPE = 'PRIMARY KEY'
) PT ON PT.TABLE_NAME = PK.TABLE_NAME
------ optional:
--ORDER BY
--1,2,3,4
--WHERE PK.TABLE_NAME='something'WHERE FK.TABLE_NAME='something'
--WHERE PK.TABLE_NAME IN ('one_thing', 'another')
--WHERE FK.TABLE_NAME IN ('one_thing', 'another')
