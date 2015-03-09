setup: paket packages

play: build app

paket:
	docker-compose run paket -c "cd .paket ; mono paket.bootstrapper.exe"

packages:
	docker-compose run paket -c "mono .paket/paket.exe install"

build:
	docker-compose up build

app:
	docker-compose up app