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


