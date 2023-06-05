USE sakila;
EXPLAIN analyze
SELECT f.title
FROM film f
INNER JOIN film_category fc ON f.film_id = fc.film_id
INNER JOIN category c ON fc.category_id = c.category_id
INNER JOIN inventory i ON i.film_id = f.film_id
INNER JOIN store s ON s.store_id = i.store_id
INNER JOIN address a ON a.address_id = s.address_id
WHERE (f.rating IN ('NC-17', 'PG-13') OR c.name = 'Horror') AND (a.address = '47 MySakila Drive')
GROUP BY f.film_id;