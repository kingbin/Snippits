/****************************************************************
* Find and rank duplicates for whatever your heart desires 
*****************************************************************/

WITH tbl_online AS (
  SELECT *,RANK () OVER (
    PARTITION BY criteriaA, criteriaB, criteriaC, criteriaD ORDER BY DateOrSomething ) AS Rnum
  FROM OurTable)
DELETE FROM tbl_online WHERE Rnum > 1
