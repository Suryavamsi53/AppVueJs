<template>
  <div class="container">
    <header>
      <h1>Data Driven UI</h1>
      <!-- Remove commented router-link section if not needed -->
    </header>
    <nav>
      <button class="show-table-button" @click="showTable('Lookup_Table')">Lookup Table</button>
      <button class="show-table-button" @click="showTable('Account_Status')">Account Status Table</button>
      <button class="show-table-button" @click="showTable('Address_Table')">Address Table</button>
      <button class="show-table-button" @click="showTable('accountType')">Account Type Table</button>
    </nav>
    <div>
      <Lookup_Table v-if="currentTable === 'Lookup_Table'" />
      <Account_Status v-if="currentTable === 'Account_Status'" />
      <Address_Table v-if="currentTable === 'Address_Table'" />
      <AccountTypeTable v-if="currentTable === 'accountType'" />
    </div>
  </div>
</template>

<script>
 
import Account_Status from './components/Account_Status.vue';
import Address_Table from './components/Address_Table.vue';
import Lookup_Table from './components/Lookup_Table.vue';
 

export default {
  name: 'App',
  components: {
    Lookup_Table,
    Account_Status,
    Address_Table,
 
   
  },
  data() {
    return {
      currentTable: '', // Initialize currentTable
    };
  },
  methods: {
    showTable(table) {
      this.currentTable = table;
    },
  },
};
</script>

<style scoped>
.container {
  font-family: sans-serif;
  background-color: #f5f5f5;
  color: #333;
  padding: 20px;
  margin-bottom: 50px; /* added margin to make room for fixed footer */
}

header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}

h1 {
  margin: 0;
  margin-top: 50px;
}

.show-table-button {
  background-color: #666;
  color: white;
  border: none;
  padding: 0.5rem 1rem;
  margin-right: 0.5rem;
  cursor: pointer;
}

.show-table-button:hover {
  background-color: #555;
}

.crud-buttons {
  margin-top: 1rem;
}

.crud-button {
  background-color: #888;
  color: white;
  border: none;
  padding: 0.5rem 1rem;
  margin-right: 0.5rem;
  cursor: pointer;
}

.crud-button:hover {
  background-color: #777;
}
</style>
