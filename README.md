# Guía de Instalación del Sistema

Esta guía detalla los pasos para instalar el entorno de desarrollo necesario para trabajar con este sistema, que incluye Visual Studio 2022 Community, SQL Server Express Edition 2022 y SQL Server Management Studio (SSMS).

---

## Paso 1: Descargar e Instalar Visual Studio 2022 Community

1. **Descarga Visual Studio 2022 Community Edition**:
   - Ve al sitio oficial de Visual Studio en [https://visualstudio.microsoft.com/](https://visualstudio.microsoft.com/).
   - Selecciona la edición **Community** y haz clic en **Download** para descargar el instalador.

2. **Instalación**:
   - Ejecuta el instalador descargado.
   - En el menú de instalación, selecciona las cargas de trabajo necesarias para tu proyecto, como **ASP.NET and web development** o **.NET desktop development**.
   - Haz clic en **Install** y espera a que se complete la instalación.

---

## Paso 2: Descargar e Instalar SQL Server Express Edition 2022

1. **Descarga SQL Server Express Edition 2022**:
   - Ve al sitio oficial de SQL Server en [https://www.microsoft.com/sql-server/sql-server-downloads](https://www.microsoft.com/sql-server/sql-server-downloads).
   - Selecciona la edición **Express** y haz clic en **Download** para descargar el instalador.

![image](https://github.com/user-attachments/assets/8c526599-f798-4a2b-8cfb-5bed5bdf863d)

2. **Instalación**:
   - Ejecuta el instalador descargado.
   - Selecciona la opción **Basic** o **Custom**, según tus necesidades.
   - Sigue las instrucciones en pantalla para completar la instalación. Asegúrate de recordar la contraseña configurada para el usuario `sa`, ya que será necesario para acceder a la base de datos.

---

## Paso 3: Instalar SQL Server Management Studio (SSMS)

1. **Descargar SQL Server Management Studio (SSMS)**:
   - Una vez completada la instalación de SQL Server Express, dirígete al sitio oficial de SSMS en [https://aka.ms/ssms](https://aka.ms/ssms).
   - Haz clic en **Download SQL Server Management Studio (SSMS)** para descargar el instalador.

2. **Instalación**:
   - Ejecuta el instalador de SSMS.
   - Sigue las instrucciones en pantalla y selecciona el directorio de instalación (se recomienda dejar el valor predeterminado).
   - Haz clic en **Install** y espera a que se complete la instalación.

---

# Paso 4: Conectarse a la base de datos

1. **Conexion**
   - Al abrir SQL Managment Server 20 les va a aparecer un menu para conectarse a una Database Engine, solo van a ingresar en Server Name: <nombre-pc>\SQLEXPRESS a continuacion un ejemplo:
   - ![image](https://github.com/user-attachments/assets/350a9ad6-dd6c-4936-afeb-09f5252c23b9)
   - Si les llega a dar error es porque le tienen que dar a Options y dan click en Trust server certificate:
   - ![image](https://github.com/user-attachments/assets/1aa56d33-1862-4cff-b305-98f85301daa9)

2. **Backup**
   - Ya que esten conectados en la base de datos van a dar click derecho en Databases y les va a dar esta opcion:
   - ![image](https://github.com/user-attachments/assets/abe9903c-3693-4b07-9690-dc7ec99929ce)
   - La van a aceptar y van a continuar.
3. **Seleccionar archivo y cargar**
   - Estando en un nuevo menu vamos a seleccionar device y darle click a los 3 puntos marcados en rojo en la siguiente imagen para elegir el archivo .bak
   - ![image](https://github.com/user-attachments/assets/eefd46b4-1126-409f-8e01-c1457356a1bc)
   - El archivo viene en el repositorio llamado DBVENTA.bak

## Paso 4: Agregar appsettings.json en apartado raiz
   - Cambiar el nombre de computadora por la suya, asi como en la base de datos, lo demas lo respetan
   - ![image](https://github.com/user-attachments/assets/1afb9b8f-aa19-424c-bbfd-8d166aefcf03)

¡Listo! Ahora tienes configurado tu entorno de desarrollo con Visual Studio 2022 y SQL Server Express Edition 2022.
