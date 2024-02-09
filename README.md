Prueba Tecnica .NET Core Andres Babativa Goyeneche 

Este proyecto utiliza .NET Core para desarrollar una API siguiendo buenas prácticas de arquitectura, diseño y principios SOLID.
Tecnologías y Patrones Utilizados:
Swagger

Swagger se integra para facilitar la documentación y prueba de la API.
Mediator Pattern y CQRS

El proyecto implementa el patrón Mediator y sigue el enfoque CQRS (Command Query Responsibility Segregation) para separar las operaciones de lectura y escritura.
Principios SOLID y Clean Code

El código sigue los principios SOLID y se adhiere a las buenas prácticas de Clean Code, priorizando la legibilidad y mantenibilidad del código.
Estructura en N-Capas

La arquitectura del proyecto sigue un enfoque de múltiples capas (N-Capas), separando claramente las responsabilidades.
Validaciones de Request y HTTP Status Codes

Se aplican patrones sólidos de validación para los objetos de solicitud (Request). Además, se manejan de manera adecuada los códigos de estado HTTP en cada respuesta de la API.
Uso de Microsoft.Extensions.Caching.Memory

Se emplea la funcionalidad de caché de la biblioteca Microsoft.Extensions.Caching.Memory para mejorar el rendimiento mediante el almacenamiento en caché de datos.
Consumo de Servicio Externo

Se consume un servicio externo utilizando la clase HttpClient, siguiendo las mejores prácticas para manejar las respuestas y posibles errores.
Pruebas Unitarias con NUnit

El proyecto incluye pruebas unitarias escritas con NUnit, abarcando principalmente los handlers que implementan la lógica de negocio utilizando el patrón Mediator.
