﻿using System.Net.Http;
using HttpClientDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace HttpClientDemo
{
	public class DataService
	{
		HttpClient client = new HttpClient();
		public DataService()
		{
		}

		/// <summary>
		/// Gets the todo items async.
		/// </summary>
		/// <returns>The todo items async.</returns>
		public async Task<List<TodoItem>> GetTodoItemsAsync()
		{
			var response = await client.GetStringAsync("http://localhost:5000/api/todo/items");
			var todoItems = JsonConvert.DeserializeObject<List<TodoItem>>(response);
			return todoItems;
		}

		/// <summary>
		/// Adds the todo item async.
		/// </summary>
		/// <returns>The todo item async.</returns>
		/// <param name="itemToAdd">Item to add.</param>
		public async Task<int> AddTodoItemAsync(TodoItem itemToAdd)
		{
			var data = JsonConvert.SerializeObject(itemToAdd);
			var content = new StringContent(data, Encoding.UTF8, "application/json");
			var response = await client.PostAsync("http://localhost:5000/api/todo/item", content);
			var result = JsonConvert.DeserializeObject<int>(response.Content.ReadAsStringAsync().Result);
			return result;
		}

		/// <summary>
		/// Updates the todo item async.
		/// </summary>
		/// <returns>The todo item async.</returns>
		/// <param name="itemIndex">Item index.</param>
		/// <param name="itemToUpdate">Item to update.</param>
		public async Task<int> UpdateTodoItemAsync(int itemIndex, TodoItem itemToUpdate)
		{
			var data = JsonConvert.SerializeObject(itemToUpdate);
			var content = new StringContent(data, Encoding.UTF8, "application/json");
			var response = await client.PutAsync(string.Concat("http://localhost:5000/api/todo/", itemIndex), content);
			return JsonConvert.DeserializeObject<int>(response.Content.ReadAsStringAsync().Result);
		}

		/// <summary>
		/// Deletes the todo item async.
		/// </summary>
		/// <returns>The todo item async.</returns>
		/// <param name="itemIndex">Item index.</param>
		public async Task DeleteTodoItemAsync(int itemIndex)
		{
			await client.DeleteAsync(string.Concat("http://localhost:5000/api/todo/", itemIndex));
		}
	}
}

