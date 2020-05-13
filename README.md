# CloudService

## Empezando

1. Se ejecuta sobre una base de datos local de **MS SQL Server** pero puedes utilizar. Podrías modificar la cadena de conexión en el fichero <a target="_blank" href="https://github.com/gabridev/CloudService/blob/master/AuthService.API/appsettings.Development.json"> appsettings.Development.json</a> en el apartado "_ConnectionStrings:AuthDb_"
2. Para crear la base de datos, debes de ejecutar el script llamado "setup.sql" que se encuentra en la siguiente ruta: <a target="_blank" href="https://github.com/gabridev/CloudService/blob/master/AuthService.RepoDB/Setup/setup.sql">AuthService.RepoDB/Setup/setup.sql</a> "AuthService.RepoDB/Setup/setup.sql"
3. Una vez completados los pasos anteriores ya puedes ejecutar la aplicación y utilizar Swagger para ir probando las llamadas.

## Mejoras
- Configuración de la base de datos con *docker* y ejecutar script para la inicialización de la base de datos("_setup.sql_")
- Definir y utilizar manejadores para los eventos de domino
- Service bus

**NOTA:** Por defecto saldrá la página de *Swagger*
