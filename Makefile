run:
	docker-compose up --build -d

clean:
	docker-compose kill
	docker-compose rm -f