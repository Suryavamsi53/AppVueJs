<template>
  <div class="address-table-container">
    <h2>Address Table</h2>
    <button class="action-button add-button" @click="showCreateForm = true">Add Address</button>
    <table class="address-table">
      <thead>
        <tr>
          <th>Address ID</th>
          <th>Account ID</th>
          <th>Address Type ID</th>
          <th>Address</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="address in addresses" :key="address.AddressID">
          <td>{{ address.AddressID }}</td>
          <td>{{ address.AccountID }}</td>
          <td>{{ address.AddressTypeID }}</td>
          <td>{{ address.Address }}</td>
          <td>
            <button class="action-button edit-button" @click="editAddress(address)">Edit</button>
            <button class="action-button delete-button" @click="deleteAddress(address.AddressID)">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>

    <!-- Create Form -->
    <div v-if="showCreateForm" class="form-container">
      <h3>Create Address</h3>
      <form @submit.prevent="createAddress">
  
    
        <!-- <label>Account ID:</label>
        <input v-model="newAddress.AccountID" type="number" required> -->
        <label>Address Type ID:</label>
        <input v-model="newAddress.AddressTypeID" type="number" required>
        <label>Address:</label>
        <input v-model="newAddress.Address" required>
        <button class="action-button submit-button" type="submit">Create</button>
        <button class="action-button cancel-button" type="button" @click="showCreateForm = false">Cancel</button>
      </form>
    </div>

    <!-- Edit Form -->
    <div v-if="showEditForm" class="form-container">
      <h3>Edit Address</h3>
      <form @submit.prevent="updateAddress">
        <label>Account ID:</label>
        <input v-model="currentAddress.AccountID" type="number" required>
        <label>Address Type ID:</label>
        <input v-model="currentAddress.AddressTypeID" type="number" required>
        <label>Address:</label>
        <input v-model="currentAddress.Address" required>
        <button class="action-button submit-button" type="submit">Update</button>
        <button class="action-button cancel-button" type="button" @click="showEditForm = false">Cancel</button>
      </form>
    </div>
  </div>
</template>

<script>
import apiService from '../backend/api'; // Adjust the path if necessary

export default {
  name: 'AddressTable',
  data() {
    return {
      addresses: [],
      accounts: [],
      showCreateForm: false,
      showEditForm: false,
      newAddress: {
        AccountID: '',
        AddressTypeID: '',
        Address: '',
      },
      currentAddress: {},
    };
  },
  methods: {
    async fetchAddresses() {
      try {
        const response = await apiService.getAddresses();
        this.addresses = response.data;
      } catch (error) {
        console.error(error);
      }
    },
    async fetchAccounts() {
      try {
        const response = await apiService.getAccounts();
        this.accounts = response.data;
      } catch (error) {
        console.error(error);
      }
    },
    async createAddress() {
      try {
        await apiService.createAddress(this.newAddress);
        this.fetchAddresses();
        this.showCreateForm = false;
        this.resetNewAddress();
      } catch (error) {
        console.error(error);
      }
    },
    editAddress(address) {
      this.currentAddress = { ...address };
      this.showEditForm = true;
    },
    async updateAddress() {
      try {
        await apiService.updateAddress(this.currentAddress.AddressID, this.currentAddress);
        this.fetchAddresses();
        this.showEditForm = false;
      } catch (error) {
        console.error(error);
      }
    },
    async deleteAddress(id) {
      try {
        await apiService.deleteAddress(id);
        this.fetchAddresses();
      } catch (error) {
        console.error(error);
      }
    },
    resetNewAddress() {
      this.newAddress = {
        AccountID: '',
        AddressTypeID: '',
        Address: '',
      };
    }
  },
  mounted() {
    this.fetchAddresses();
    this.fetchAccounts();
  },
};
</script>

<style scoped>
.address-table-container {
  max-width: 800px;
  margin: auto;
}

.action-button {
  margin: 5px;
  padding: 10px 15px;
  border: none;
  color: white;
  cursor: pointer;
}

.add-button {
  background-color: #4CAF50;
}

.edit-button {
  background-color: #2196F3;
}

.delete-button {
  background-color: #f44336;
}

.submit-button {
  background-color: #4CAF50;
}

.cancel-button {
  background-color: #9E9E9E;
}

.address-table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 20px;
}

.address-table th, .address-table td {
  border: 1px solid #ddd;
  padding: 8px;
}

.address-table th {
  background-color: #f2f2f2;
}

.form-container {
  margin-top: 20px;
  padding: 20px;
  border: 1px solid #ddd;
  background-color: #f9f9f9;
}

.form-container label {
  display: block;
  margin-top: 10px;
}

.form-container input {
  width: calc(100% - 20px);
  padding: 8px;
  margin-top: 5px;
}
</style>
