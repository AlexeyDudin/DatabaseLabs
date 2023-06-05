USE sakila;
EXPLAIN analyze
SELECT a.first_name, a.last_name
FROM actor a
INNER JOIN film_actor fa ON fa.actor_id = a.actor_id
INNER JOIN film f ON f.film_id = fa.film_id
WHERE f.rating IN ('G','PG') AND a.last_name = 'HOPKINS'
GROUP BY fa.actor_id
HAVING count(*) >= 10
-- SELECT *
-- FROM sakila.actor AS a
-- WHERE a.last_name = 'HOPKINS' AND a.actor_id in
-- (
-- 	SELECT actor_id
--     FROM sakila.film_actor AS fa
--     WHERE fa.film_id IN
--     (
-- 		SELECT film_id 
-- 		FROM sakila.film
-- 		WHERE rating = 'G' OR rating = 'PG'
--     )
--     GROUP BY actor_id 
--     HAVING COUNT(*) >=10
-- )