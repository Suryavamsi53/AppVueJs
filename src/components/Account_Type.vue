<!-- src/components/AccountStatusList.vue -->
<template>
    <div>
        <h2>Account Status Table</h2>
        <button @click="showCreateForm = true">Add Account Status</button>
        <table>
            <thead>
                <tr>
                    <th>Status ID</th>
                    <th>Status Name</th>
                    <th>Description</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="status in accountStatuses" :key="status.StatusID">
                    <td>{{ status.StatusID }}</td>
                    <td>{{ status.StatusName }}</td>
                    <td>{{ status.Description }}</td>
                    <td>
                        <button @click="editAccountStatus(status)">Edit</button>
                        <button @click="deleteAccountStatus(status.StatusID)">Delete</button>
                    </td>
                </tr>
            </tbody>
        </table>

        <!-- Create Form -->
        <div v-if="showCreateForm">
            <h3>Create Account Status</h3>
            <form @submit.prevent="createAccountStatus">
                <label>Status Name:</label>
                <input v-model="newAccountStatus.StatusName" required>
                <label>Description:</label>
                <input v-model="newAccountStatus.Description" required>
                <button type="submit">Create</button>
                <button @click="showCreateForm = false">Cancel</button>
            </form>
        </div>

        <!-- Edit Form -->
        <div v-if="showEditForm">
            <h3>Edit Account Status</h3>
            <form @submit.prevent="updateAccountStatus">
                <label>Status Name:</label>
                <input v-model="currentAccountStatus.StatusName" required>
                <label>Description:</label>
                <input v-model="currentAccountStatus.Description" required>
                <button type="submit">Update</button>
                <button @click="showEditForm = false">Cancel</button>
            </form>
        </div>
    </div>
</template>

<script>
import apiService from '../backend/api';

export default {
    data() {
        return {
            accountStatuses: [],
            showCreateForm: false,
            showEditForm: false,
            newAccountStatus: {
                StatusName: '',
                Description: ''
            },
            currentAccountStatus: {}
        };
    },
    methods: {
        async fetchAccountStatuses() {
            try {
                const response = await apiService.getAccountStatuses();
                this.accountStatuses = response.data;
            } catch (error) {
                console.error(error);
            }
        },
        async createAccountStatus() {
            try {
                await apiService.createAccountStatus(this.newAccountStatus);
                this.fetchAccountStatuses();
                this.showCreateForm = false;
                this.newAccountStatus = {
                    StatusName: '',
                    Description: ''
                };
            } catch (error) {
                console.error(error);
            }
        },
        editAccountStatus(status) {
            this.currentAccountStatus = { ...status };
            this.showEditForm = true;
        },
        async updateAccountStatus() {
            try {
                await apiService.updateAccountStatus(this.currentAccountStatus.StatusID, this.currentAccountStatus);
                this.fetchAccountStatuses();
                this.showEditForm = false;
            } catch (error) {
                console.error(error);
            }
        },
        async deleteAccountStatus(id) {
            try {
                await apiService.deleteAccountStatus(id);
                this.fetchAccountStatuses();
            } catch (error) {
                console.error(error);
            }
        }
    },
    mounted() {
        this.fetchAccountStatuses();
    }
};
</script>

<style scoped>
/* Add your styles here */
</style>
