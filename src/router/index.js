// src/router/index.js
import { createRouter, createWebHistory } from 'vue-router';
import LookupList from '../components/LookupList.vue';
import AccountList from '../components/AccountList.vue';
import AddressList from '../components/AddressList.vue';
import Account_Status from '../components/Account_Status.vue';
// Import other components as needed

Vue.use(router);


// const routes = [
//     { path: '/lookups', component: LookupList },
//     { path: '/accounts', component: AccountList },
//     { path: '/addresses', component: AddressList },
//     { path: '/account-statuses', component: AccountStatusList },
//     // Add other routes as needed
// ];
export default new Router({
    routes: [
      {
        path: '/',
        name: 'Home',
        component: Database,
      },
      {
        path: '/database',
        name: 'Database',
        component: Database,
      },
      {
        path: '/account-status',
        name: 'AccountStatus',
        component: AccountStatus
      },
    ],

// const router = createRouter({
//     history: createWebHistory(),
//     routes,

});
// export default router;
