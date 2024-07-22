// src/backend/api.js

import axios from 'axios';

const API_BASE_URL = 'http://localhost:3081/api'; // Base URL for your backend API

// Function to construct API URL for a given table
function getApiUrl(tableName) {
  return `${API_BASE_URL}/${tableName}`;
}

export default {
  // Fetch all records from a table
  async fetchRecords(tableName) {
    try {
      const response = await axios.get(getApiUrl(tableName));
      return response.data;
    } catch (error) {
      console.error(`Error fetching records from ${tableName}:`, error);
      throw error;
    }
  },

  // Add a new record to a table
  async addRecord(tableName, newRecord) {
    try {
      const response = await axios.post(getApiUrl(tableName), newRecord);
      return response.data;
    } catch (error) {
      console.error(`Error adding record to ${tableName}:`, error);
      throw error;
    }
  },

  // Update an existing record in a table
  async updateRecord(tableName, id, updatedRecord) {
    try {
      const response = await axios.put(`${getApiUrl(tableName)}/${id}`, updatedRecord);
      return response.data;
    } catch (error) {
      console.error(`Error updating record in ${tableName}:`, error);
      throw error;
    }
  },

  // Delete a record from a table
  async deleteRecord(tableName, id) {
    try {
      await axios.delete(`${getApiUrl(tableName)}/${id}`);
    } catch (error) {
      console.error(`Error deleting record from ${tableName}:`, error);
      throw error;
    }
  },

  // Specific methods for the tables
  async getLookups() {
    return this.fetchRecords('lookups');
  },
  async addLookup(newLookup) {
    return this.addRecord('lookups', newLookup);
  },
  async updateLookup(id, updatedLookup) {
    return this.updateRecord('lookups', id, updatedLookup);
  },
  async deleteLookup(id) {
    return this.deleteRecord('lookups', id);
  },
  async getAccounts() {
    return this.fetchRecords('account_status'); // Adjust table name as needed
  },
  async addAccount(newAccount) {
    return this.addRecord('account_status', newAccount); // Adjust table name as needed
  },
  async updateAccount(id, updatedAccount) {
    return this.updateRecord('account_status', id, updatedAccount); // Adjust table name as needed
  },
  async deleteAccount(id) {
    return this.deleteRecord('account_status', id); // Adjust table name as needed
  },
  async getAddresses() {
    return this.fetchRecords('address_table'); // Adjust table name as needed
  },
  async addAddress(newAddress) {
    return this.addRecord('address_table', newAddress); // Adjust table name as needed
  },
  async updateAddress(id, updatedAddress) {
    return this.updateRecord('address_table', id, updatedAddress); // Adjust table name as needed
  },
  async deleteAddress(id) {
    return this.deleteRecord('address_table', id); // Adjust table name as needed
  }
};
