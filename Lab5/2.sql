explain analyze
-- SELECT *
-- FROM sakila.actor a
-- WHERE a.first_name = 'CUBA' OR a.last_name = 'ALLEN'
-- Доработать индексыactoractor
SELECT *
FROM sakila.actor a
WHERE a.first_name = 'CUBA'
UNION DISTINCT
SELECT *
FROM sakila.actor a
WHERE a.last_name = 'ALLEN'