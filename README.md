# DevTest

Proyecto "Prueba t�cnica" para Homini

# Desarrollado por

Alejandro Castro Agudelo - alejoagu26@gmail.com

# Instrucciones

[Aqu�](./recursos/doc/prueba-tecnica.pdf) encontrar�s la prueba t�cnica

# Propuesta de soluci�n

![diagrama](./recursos/design/arquitectura.drawio.png)

# Base de datos

[Aqu�](./recursos/sql/scriptbasedatos.sql) encontrar�s el script para la creaci�n de la base de datos

# Backend

Para ejecutar el proyecto se deben seguir los siguientes pasos
- Actualizar el appsettings.json con la conexi�n a la base de datos
- Se utiliz� desarrollo code first, para usar el proyecto se puede ejecutar el script de base de datos o se puede crear la migraci�n

      add-migration MigracionInicial
- Actualizar base de datos

      update-database
- Compilar el proyecto y ejecutarlo
