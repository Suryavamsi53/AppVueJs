<template>
  <div>
    <!-- Navbar -->
    <nav class="navbar">
      <div class="container">
        <!-- CRUD Operation Buttons -->
        <div v-if="showCrudButtons" class="crud-buttons">
          <button class="crud-button" @click="toggleForm('add')">Add Data</button>
          <button class="crud-button" @click="toggleForm('delete')">Delete Data</button>
          <button class="crud-button" @click="toggleTable">Display</button>
        </div>
      </div>
    </nav>

    <!-- Content Area -->
    <div class="content">
      <!-- Error message display -->
      <h3 v-if="errorMsg" class="error-message">{{ errorMsg }}</h3>

      <!-- Data Table -->
      <div class="data-container" v-if="showTable">
        <table class="data-table">
          <thead>
            <tr>
              <th>Lookup Id</th>
              <th>Lookup Code</th>
              <th>Lookup Type</th>
              <th>Lookup Desc</th>
              <th>Is Active</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="lookup in paginatedLookups" :key="lookup.Lookup_Id">
              <td>{{ lookup.Lookup_Id }}</td>
              <td>{{ lookup.Lookup_Code }}</td>
              <td>{{ lookup.Lookup_Type }}</td>
              <td>{{ lookup.Lookup_Desc }}</td>
              <td>{{ lookup.Is_Active }}</td>
              <td>
                <button class="action-button edit-button" @click="editLookup(lookup)">Edit</button>
                <button class="action-button delete-button" @click="deleteLookup(lookup.Lookup_Id)">Delete</button>
              </td>
            </tr>
          </tbody>
        </table>
        <div class="pagination" v-if="lookups.length > pageSize">
          <button class="pagination-button" @click="prevPage" :disabled="currentPage === 1">Previous</button>
          <button class="pagination-button" @click="nextPage" :disabled="currentPage === totalPages">Next</button>
          <span>Page {{ currentPage }} of {{ totalPages }}</span>
        </div>
      </div>

      <!-- Add Lookup Form -->
      <div class="form-container" v-if="showAddForm">
        <h2>Add New Lookup</h2>
        <form @submit.prevent="addLookup">
          <input v-model="newLookup.Lookup_Id" placeholder="Lookup ID">
          <input v-model="newLookup.Lookup_Code" placeholder="Lookup Code">
          <input v-model="newLookup.Lookup_Type" placeholder="Lookup Type">
          <input v-model="newLookup.Lookup_Desc" placeholder="Lookup Description">
          <input v-model="newLookup.Is_Active" placeholder="Is Active">
          <button class="action-button submit-button" type="submit">Add</button>
          <button class="action-button cancel-button" type="button" @click="cancelAdd">Cancel</button>
        </form>
      </div>

      <!-- Update Lookup Form -->
      <div class="form-container" v-if="showUpdateForm">
        <h2>Update Lookup</h2>
        <form @submit.prevent="updateLookup">
          <input v-model="newLookup.Lookup_Id" placeholder="Lookup ID" readonly>
          <input v-model="newLookup.Lookup_Code" placeholder="Lookup Code">
          <input v-model="newLookup.Lookup_Type" placeholder="Lookup Type">
          <input v-model="newLookup.Lookup_Desc" placeholder="Lookup Description">
          <input v-model="newLookup.Is_Active" placeholder="Is Active">
          <button class="action-button submit-button" type="submit">Update</button>
          <button class="action-button cancel-button" type="button" @click="cancelUpdate">Cancel</button>
        </form>
      </div>

      <!-- Delete Lookup Form -->
      <div class="form-container" v-if="showDeleteForm">
        <h2>Delete Lookup</h2>
        <form @submit.prevent="deleteLookupById">
          <input v-model="currentLookupId" placeholder="Enter Lookup ID to Delete">
          <button class="action-button submit-button" type="submit">Delete</button>
          <button class="action-button cancel-button" type="button" @click="cancelDelete">Cancel</button>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import apiService from '../backend/api';

export default {
  name: 'Databases',
  data() {
    return {
      lookups: [],
      currentPage: 1,
      pageSize: 5,
      errorMsg: '',
      newLookup: {
        Lookup_Id: null,
        Lookup_Code: null,
        Lookup_Type: '',
        Lookup_Desc: '',
        Is_Active: ''
      },
      currentLookupId: '',
      showAddForm: false,
      showUpdateForm: false,
      showDeleteForm: false,
      showTable: true,
      showCrudButtons: true
    };
  },
  computed: {
    totalPages() {
      return Math.ceil(this.lookups.length / this.pageSize);
    },
    paginatedLookups() {
      const startIndex = (this.currentPage - 1) * this.pageSize;
      return this.lookups.slice(startIndex, startIndex + this.pageSize);
    }
  },
  async created() {
    await this.fetchLookups();
  },
  methods: {
    nextPage() {
      if (this.currentPage < this.totalPages) {
        this.currentPage++;
      }
    },
    prevPage() {
      if (this.currentPage > 1) {
        this.currentPage--;
      }
    },
    toggleTable() {
      this.showTable = !this.showTable;
      if (this.showTable) {
        this.fetchLookups();
      }
    },
    toggleCrudButtons() {
      this.showCrudButtons = !this.showCrudButtons;
    },
    toggleForm(formType) {
      this.showAddForm = formType === 'add';
      this.showUpdateForm = formType === 'update';
      this.showDeleteForm = formType === 'delete';
    },
    async fetchLookups() {
      try {
        const response = await apiService.getLookups(); // Ensure this method exists in apiService
        this.lookups = response.data;
      } catch (error) {
        console.error('Error fetching lookups:', error);
        this.errorMsg = 'Failed to fetch lookups. Please try again.';
      }
    },
    async addLookup() {
      try {
        const response = await apiService.addLookup(this.newLookup); // Ensure this method exists in apiService
        this.lookups.push(response.data);
        this.resetForm();
        this.showAddForm = false;
      } catch (error) {
        console.error('Error adding lookup:', error);
        this.errorMsg = 'Failed to add lookup. Please try again.';
      }
    },
    async updateLookup() {
      try {
        await apiService.updateLookup(this.newLookup.Lookup_Id, this.newLookup); // Ensure this method exists in apiService
        this.fetchLookups();
        this.resetForm();
        this.showUpdateForm = false;
      } catch (error) {
        console.error('Error updating lookup:', error);
        this.errorMsg = 'Failed to update lookup. Please try again.';
      }
    },
    async deleteLookup(id) {
      try {
        await apiService.deleteLookup(id); // Ensure this method exists in apiService
        this.fetchLookups();
      } catch (error) {
        console.error('Error deleting lookup:', error);
        this.errorMsg = 'Failed to delete lookup. Please try again.';
      }
    },
    async deleteLookupById() {
      try {
        await apiService.deleteLookup(this.currentLookupId); // Ensure this method exists in apiService
        this.fetchLookups();
        this.currentLookupId = '';
        this.showDeleteForm = false;
      } catch (error) {
        console.error('Error deleting lookup:', error);
        this.errorMsg = 'Failed to delete lookup. Please try again.';
      }
    },
    cancelAdd() {
      this.resetForm();
      this.showAddForm = false;
    },
    cancelUpdate() {
      this.resetForm();
      this.showUpdateForm = false;
    },
    cancelDelete() {
      this.currentLookupId = '';
      this.showDeleteForm = false;
    },
    resetForm() {
      this.newLookup = {
        Lookup_Id: null,
        Lookup_Code: null,
        Lookup_Type: '',
        Lookup_Desc: '',
        Is_Active: ''
      };
      this.errorMsg = ''; // Clear any previous error messages
    }
  }
};
</script>
<style scoped>
.navbar {
  background-color: #333;
  color: white;
  padding: 1rem;
}

.container {
  display: flex;
  justify-content: flex-start; /* Align items to the left */
  align-items: center; /* Center items vertically */
  gap: 10px; /* Add some spacing between items */
}

.crud-buttons {
  display: flex;
  gap: 10px;
}

.crud-button {
  padding: 10px;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s;
}

/* .crud-button:hover {
  background-color: #555;
} */

.content {
  padding: 2rem;
}

.error-message {
  color: red;
}

.data-container {
  margin-top: 2rem;
}

.data-table {
  width: 100%;
  border-collapse: collapse;
}

.data-table th, .data-table td {
  border: 1px solid #ddd;
  padding: 0.75rem;
}

.action-button {
  padding: 0.5rem 1rem;
  border: none;
  cursor: pointer;
  transition: background-color 0.3s;
}

.edit-button {
  background-color: #4CAF50;
  color: white;
}

.edit-button:hover {
  background-color: #45a049;
}

.delete-button {
  background-color: #f44336;
  color: white;
}

.delete-button:hover {
  background-color: #da190b;
}

.pagination {
  margin-top: 1rem;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.pagination-button {
  padding: 0.5rem 1rem;
  background-color: #888;
  color: white;
  border: none;
  cursor: pointer;
  transition: background-color 0.3s;
}

.pagination-button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

.pagination-button:not(:disabled):hover {
  background-color: #555;
}

.form-container {
  margin-top: 2rem;
}

.form-container h2 {
  margin-bottom: 1rem;
}

.form-container input {
  display: block;
  margin-bottom: 1rem;
  padding: 0.5rem;
  width: 100%;
  box-sizing: border-box;
}

.submit-button {
  background-color: #4CAF50;
  color: white;
}

.submit-button:hover {
  background-color: #45a049;
}

.cancel-button {
  background-color: #f44336;
  color: white;
}

.cancel-button:hover {
  background-color: #da190b;
}
</style>
