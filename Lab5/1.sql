USE sakila;
EXPLAIN analyze
SELECT a.last_name, a.first_name
FROM actor a
Inner join film_actor fa ON a.actor_id = fa.actor_id
INNER JOIN film f ON f.film_id = fa.film_id
WHERE f.rating = 'G'
GROUP BY a.actor_id
ORDER BY a.last_name, a.first_name;

-- SELECT *
-- FROM sakila.actor
-- LEFT JOIN
-- (
-- 	sakila.film_actor fa
--     LEFT JOIN
--     {
-- 		sakila.film f
--         (SELECT *
--         FROM f
--         WHERE rating = 'G')
--     } ON fa.film_id = f.id
-- ) ON a.id = fa.actor_id
-- Explain analyze
-- SELECT * 
-- FROM sakila.actor
-- WHERE actor_id in
-- (
-- 	SELECT actor_id
-- 	FROM sakila.film_actor
-- 	WHERE film_id IN
-- 		(
-- 		SELECT film_id
--         FROM sakila.film
-- 		WHERE rating = 'G'
-- 		)
-- )
-- ORDER BY last_name, first_name;