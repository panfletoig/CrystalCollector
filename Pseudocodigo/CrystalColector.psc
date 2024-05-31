Función Start(Argumentos)
	Definir nombre Como Cadena
	Definir genero Como Cadena
	Escribir 'Nombre:'
	Leer nombre
	Escribir 'Avatar:'
	Leer genero
	Definir play Como Lógico
	Definir nivelActual Como Entero
	Definir vidas Como Entero
	Definir troll Como Entero
	Definir trivia Como Entero
	Definir gema Como Entero
	Definir cristales Como Entero
	Definir portal Como Entero
	play <- Verdadero
	nivelActual <- 1
	cristales <- 0
	Mientras play Hacer
		// Aqui se imprimira
		// el Nivel
		// Stats
		// Caja recolectora
		Escribir 'Nivel'
		Escribir 'Estado del personaje'
		Escribir 'Caja Recolectora'
		// Esto se encargara el programa no el usuario
		Escribir 'Hay portal 1. si 2. no'
		Leer portal
		Si portal==1 Entonces
			Si cristales>=6 Entonces
				nivelActual <- nivelActual+1
			FinSi
		FinSi
		Si portal<>1 Entonces
			// Esto se encargara el programa no el usuario
			Escribir 'Hay gema 1. si 2. no'
			Leer gema
			Si gema==1 Entonces
				cristales <- cristales+1
			FinSi
		FinSi
		Si portal<>1 Y gema<>1 Entonces
			// Esto se encargara el programa no el usuario
			// Se realiza la trivia si hay un troll en la posicion
			Escribir 'Hay un troll 1. si 2. no'
			Leer troll
			Si troll==1 Entonces
				Escribir 'Gano la trivia 1. si 2. no'
				Leer trivia
				Si trivia==1 Entonces
					vidas <- vidas+1
					Si vidas>=3 Entonces
						vidas <- 3
					FinSi
				SiNo
					Si trivia==2 Entonces
						vidas <- vidas-1
						actual <- actual-1
					FinSi
				FinSi
			FinSi
		FinSi
		// Aqui se haria el conteo de puntos
		// Aqui si se permite el movimiento
		// Si gano o perdio
		Si (nivelActual>5 O vidas<0) Entonces
			play <- Falso
			Si (vidas<0) Entonces
				Escribir 'PERDISTE'
			SiNo
				Escribir 'GANASTE'
			FinSi
			Escribir 'Estado del personaje'
		FinSi
	FinMientras
FinFunción

Algoritmo CristalCollector
	Definir op Como Entero
	Mientras op<>4 Hacer
		// Imprimir el menu
		Escribir 'Menu'
		Escribir '1. Iniciar Partida'
		Escribir '2 Instruciones..3 Informacion..4 Salir'
		Leer op
		// Segun la opcion
		Según op Hacer
			1:
				// metodo Start
				Start(Argumentos)
			2:
				Escribir 'Informacion'
			3:
				Escribir 'Informacion'
			4:
				Escribir 'Saliendo'
		FinSegún
	FinMientras
FinAlgoritmo
