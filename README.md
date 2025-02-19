# API RESTful para Gestionar una Librería Digital

## Enunciado del Challenge

Este proyecto consiste en la creación de una API RESTful para gestionar una librería digital. Los modelos principales de la librería son:

- **Libro**: Representa la información de los libros disponibles en la librería.
- **Autor**: Representa los autores de los libros.
- **Categoría**: Representa las categorías de los libros.

La API debe contar con las siguientes funcionalidades:

- CRUD completo para cada entidad (Libro, Autor, Categoría).
- Paginación para la consulta de los libros.
- Filtrado de libros por autor, categoría y título.

### Tecnologías

- **Entity Framework**: Para la persistencia de datos.
- **LINQ**: Para realizar consultas eficientes a la base de datos.
- **Patrón Repository**: Para controlar los datos de las entidades.
- **Patrón Service**: Para manejar la lógica de negocio.

### Base de Datos

- **PostgreSQL**: Para el almacenamiento de datos de las entidades.

## Funcionalidades Adicionales

- Paginación: Los resultados de las consultas pueden ser segmentados para evitar la sobrecarga de la base de datos.
- Filtrado: Permite a los usuarios filtrar libros por autor, categoría y título, mejorando la experiencia de búsqueda.
