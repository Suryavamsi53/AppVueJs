<template>
  <div>
    <!-- Navbar -->
    <nav>
      <div v-if="showCrudButtons" class="crud-buttons">
        <button class="crud-button" @click="toggleForm('add')">Add Data</button>
        <button class="crud-button" @click="toggleForm('delete')">Delete Data</button>
        <button class="crud-button" @click="toggleTable">Display</button>
      </div>
    </nav>

    <!-- Data Table -->
    <div v-if="showTable" class="data-container">
      <table class="data-table">
        <thead>
          <tr>
            <th>AccId</th>
            <th>AccountNumber</th>
            <th>AccountStatus_id</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="account in paginatedAccounts" :key="account.AccId">
            <td>{{ account.AccId }}</td>
            <td>{{ account.AccountNumber }}</td>
            <td>{{ account.AccountStatus_id }}</td>
            <td>
              <button class="action-button delete-button" @click="deleteAccount(account.AccId)">Delete</button>
            </td>
          </tr>
        </tbody>
      </table>
      <div class="pagination" v-if="accounts.length > pageSize">
        <button class="pagination-button" @click="prevPage" :disabled="currentPage === 1">Previous</button>
        <button class="pagination-button" @click="nextPage" :disabled="currentPage === totalPages">Next</button>
        <span>Page {{ currentPage }} of {{ totalPages }}</span>
      </div>
    </div>

    <!-- Add Account Form -->
    <div class="form-container" v-if="showAddForm">
      <h2>Add New Account</h2>
      <form @submit.prevent="addAccount">
        <input v-model="newAccount.AccId" placeholder="AccId" required>
        <input v-model="newAccount.AccountNumber" placeholder="Account Number" required>
        <input v-model="newAccount.AccountStatus_id" placeholder="Account Status ID" required>
        <button class="action-button submit-button" type="submit">Add</button>
        <button class="action-button cancel-button" type="button" @click="cancelAdd">Cancel</button>
      </form>
    </div>

    <!-- Delete Account Form -->
    <div class="form-container" v-if="showDeleteForm">
      <h2>Delete Account</h2>
      <form @submit.prevent="deleteAccountById">
        <input v-model="currentAccountId" placeholder="Enter AccId to Delete" required>
        <button class="action-button submit-button" type="submit">Delete</button>
        <button class="action-button cancel-button" type="button" @click="cancelDelete">Cancel</button>
      </form>
    </div>
  </div>
</template>

<script>
import apiService from '../backend/api';

export default {
  data() {
    return {
      accounts: [],
      currentPage: 1,
      pageSize: 5,
      newAccount: {
        AccId: '',
        AccountNumber: '',
        AccountStatus_id: ''
      },
      currentAccountId: '',
      showAddForm: false,
      showDeleteForm: false,
      showTable: true,
      showCrudButtons: true,
    };
  },
  computed: {
    totalPages() {
      return Math.ceil(this.accounts.length / this.pageSize);
    },
    paginatedAccounts() {
      const startIndex = (this.currentPage - 1) * this.pageSize;
      return this.accounts.slice(startIndex, startIndex + this.pageSize);
    }
  },
  methods: {
    toggleTable() {
      this.showTable = !this.showTable;
      if (this.showTable) {
        this.fetchAccounts();
      }
    },
    toggleForm(formType) {
      this.showAddForm = formType === 'add';
      this.showDeleteForm = formType === 'delete';
      this.showCrudButtons = false;
    },
    async fetchAccounts() {
      try {
        const response = await apiService.getAccounts();
        this.accounts = response.data;
      } catch (error) {
        console.error('Error fetching accounts:', error);
      }
    },
    async addAccount() {
      try {
        const response = await apiService.addAccount(this.newAccount);
        this.accounts.push(response.data);
        this.resetForm();
        this.showAddForm = false;
        this.showCrudButtons = true;
        this.fetchAccounts();
      } catch (error) {
        console.error('Error adding account:', error);
      }
    },
    async deleteAccount(id) {
      try {
        await apiService.deleteAccount(id);
        this.fetchAccounts();
      } catch (error) {
        console.error('Error deleting account:', error);
      }
    },
    async deleteAccountById() {
      try {
        await apiService.deleteAccount(this.currentAccountId);
        this.currentAccountId = '';
        this.showDeleteForm = false;
        this.showCrudButtons = true;
        this.fetchAccounts();
      } catch (error) {
        console.error('Error deleting account:', error);
      }
    },
    cancelAdd() {
      this.resetForm();
      this.showAddForm = false;
      this.showCrudButtons = true;
    },
    cancelDelete() {
      this.currentAccountId = '';
      this.showDeleteForm = false;
      this.showCrudButtons = true;
    },
    resetForm() {
      this.newAccount = {
        AccId: '',
        AccountNumber: '',
        AccountStatus_id: ''
      };
    },
    prevPage() {
      if (this.currentPage > 1) {
        this.currentPage--;
      }
    },
    nextPage() {
      if (this.currentPage < this.totalPages) {
        this.currentPage++;
      }
    }
  },
  mounted() {
    this.fetchAccounts();
  }
};
</script>
 


<style scoped>
/* Navbar */
nav {
   
  background-color: #333;
  color: white;
  padding: 1rem;
 
}

/* CRUD Buttons */
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

.crud-button:hover {
  background-color: #0056b3;
}

/* Data Table */
.data-container {
  margin-top: 2rem;
}

.data-table {
  width: 100%;
  border-collapse: collapse;
}

.data-table th,
.data-table td {
  padding: 10px;
  border: 1px solid #ddd;
}

.data-table th {
  background-color: #f2f2f2;
}

.action-button {
  padding: 0.5rem 1rem;
  border: none;
  cursor: pointer;
  transition: background-color 0.3s;
}

.delete-button {
  background-color: #f44336;
  color: white;
}

.delete-button:hover {
  background-color: #da190b;
}

/* Pagination */
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

/* Forms */
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
