USE sakila;
EXPLAIN ANALYZE
SELECT f.title
FROM film f
INNER JOIN inventory i ON i.film_id = f.film_id
INNER JOIN store s ON s.store_id = i.store_id
INNER JOIN address a ON a.address_id = s.address_id
INNER JOIN rental r ON r.inventory_id = i.inventory_id
WHERE (a.address = '28 MySQL Boulevard') AND (r.rental_date IS NOT NULL)
GROUP BY f.film_id;