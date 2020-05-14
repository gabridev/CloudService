# CloudService

Es una aplicación de prueba que ha sido desarrollada utilizando patrones como DDD, programación reactiva, principios SOLID, Clean Code y para el accesso a datos se ha utilizado un micro ORM llamado: _RepoDB_. 

## Empezando

1. Se ejecuta sobre una base de datos local de **MS SQL Server** pero puedes utilizar otra instancia de SQL Server. Para ello, deberías modificar la cadena de conexión que se encuentra en el fichero <a target="_blank" href="https://github.com/gabridev/CloudService/blob/master/AuthService.API/appsettings.Development.json"> _appsettings.Development.json_</a> en el apartado "_ConnectionStrings:AuthDb_"
2. Para crear la base de datos y las tablas, debes de ejecutar el script llamado "_setup.sql_" que se encuentra en la siguiente ruta: <a target="_blank" href="https://github.com/gabridev/CloudService/blob/master/AuthService.RepoDB/Setup/setup.sql">_AuthService.RepoDB/Setup/setup.sql_</a>
3. Una vez completados los pasos anteriores, ya puedes ejecutar la aplicación y utilizar Swagger para ir probando las llamadas.

## Mejoras
- Configuración de la base de datos con *docker* y ejecutar script para la inicialización de la base de datos("_setup.sql_")
- Programación reactiva 
  * Definir y utilizar manejadores para los eventos de domino
  * Buses de servicio para la comunicación entre microservicios, poer ejemplo, _Service bus_, _RabbitQM_

**NOTA:** Por defecto saldrá la página de *Swagger*. Se debe ejecutar usando el IIS Express o el ISS, ya que la parte de la base de datos  con DOcker, no ha sido configurada.
