# EntityFramework2023

# Models
En utilisant un ORM en ==CodeFirst==, il va falloir définir nos classes de modèle de donnée.
Elles sont définies par leurs clés en table et leurs potentielle relation.

En extrapolant une application d'e-commerce nous aurions besoin d'un modèle d'article.
Qui contiendra un ID, un label, une description, ce même article aura une relation avec un autre modèle qui définira les catégories. Donc on aura une liste de catégories qui contient une liste d'article.

# modelBuilder
Le fichier NortwindContext.cs contient les DbSet ainsi que les modelBuilder.

DbSet est la méthode d'EntityFramework pour assimiler la classe en table SQL
Il contient aussi la méthode de connexion a la DB.
Post création des tables il nous est possible de définir les relations dans ce même script en utilisant les modelBuilder.

# Controllers
Nos controleurs ont été générés à travers le package nugget Microsoft.AspNetCore.Mvc qui permets la génération de controller CRUD "simple".

# Startup.cs
Startup.cs défini la configuration des pipeline de requête et des services utilisés par l'application. C'est ici qu'on retrouve l'instanciation de Swagger.

# Utilisation de l'API
L'api permets les opérations CRUD sur chaque table. 

Les endpoints accepteront un format JSON ayant pourr champs les clés de la table visée ainsi que l'id de la table en relation si une clé étrangère est existante sur le modèle ciblé.

# Commandes à écécuter / Changement de modèle
 - dotnet run => Lance le projet et l'API
 - dotnet ef database update => met à jour la BDD après modification d'un modele (database initialisée avec : dotnet ef migrations add InitialCreate)