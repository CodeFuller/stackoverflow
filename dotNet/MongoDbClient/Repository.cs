using System.Collections.Generic;
using MongoDB.Driver;

namespace MongoDbClient
{
	public class Repository<TDocument> where TDocument : Document
	{
		private IMongoCollection<TDocument> Collection { get; }

		public Repository(IMongoClient mongoClient, string databaseName, string collectionName)
		{
			var database = mongoClient.GetDatabase(databaseName);
			Collection = database.GetCollection<TDocument>(collectionName);
		}

		public void AddDocument(TDocument document)
		{
			Collection.InsertOne(document);
		}

		public IEnumerable<TDocument> GetDocuments()
		{
			return Collection.AsQueryable();
		}

		public void UpdateDocument(TDocument document)
		{
			Collection.ReplaceOne(GetDocumentFilter(document.Id), document);
		}

		public void UpsertDocument(TDocument document)
		{
			var options = new UpdateOptions {IsUpsert = true};
			Collection.ReplaceOne(GetDocumentFilter(document.Id), document, options);
		}

		public void DeleteDocument(int documentId)
		{
			Collection.DeleteOne(GetDocumentFilter(documentId));
		}

		private FilterDefinition<TDocument> GetDocumentFilter(int documentId)
		{
			return Builders<TDocument>.Filter.Eq(doc => doc.Id, documentId);
		}
	}
}
