import { createApp } from 'vue'
import App from './App.vue'

createApp(App).mount('#app')



import Vue from 'vue';
import App from './App.vue';
import axios from 'axios';

Vue.config.productionTip = false;

Vue.prototype.$http = axios.create({
  baseURL: 'http://localhost:8733/Design_Time_Addresses/YourNamespace/YourService/',
  headers: {
    'Content-Type': 'application/json'
  }
});

new Vue({
  render: h => h(App),
}).$mount('#app');
