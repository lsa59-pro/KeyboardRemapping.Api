## Technologies utilisées

* C#
* ASP.NET Core 9
* Entity Framework Core
* SQLite
* Swagger / OpenAPI

## Installation

Cloner le repository puis ouvrir la solution `KeyboardRemapping.sln` avec Visual Studio 2022.

La solution peut ensuite être compilée et lancée directement depuis Visual Studio.

* La base SQLite a été créée automatiquement à partir des migrations Entity Framework Core.
* La base de données se trouve dans le projet et se nomme `keyboard-remapping.db`.
* Les données initiales du clavier sont ajoutées au démarrage de l'application.

## Swagger

Une fois l'application démarrée :

http://localhost:5268/swagger

## Tests

Les appels API peuvent être testés directement depuis Swagger.

Exemple de parcours :

1. Récupérer les claviers avec `GET /api/keyboards`.
2. Créer un clavier avec `POST /api/keyboards`.
3. Récupérer ses mappings avec `GET /api/keyboards/{keyboardId}/mappings`.
4. Modifier les mappings avec `PUT /api/keyboards/{keyboardId}/mappings`.
5. Vérifier le résultat avec un nouveau `GET`.
6. Supprimer les mappings avec `DELETE /api/keyboards/{keyboardId}/mappings`.

