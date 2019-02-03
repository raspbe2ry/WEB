import Vue from 'vue/dist/vue.js'
import Home from './components/Home.vue'
import Trainings from './components/Trainings.vue'
import Gallery from './components/Gallery.vue'
import NavElement from './components/NavElement'
import VueRouter from 'vue-router'

import BootstrapVue from 'bootstrap-vue'
Vue.use(BootstrapVue);
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
 
Vue.config.productionTip = false

Vue.use(VueRouter)

const routes = [
  { path: '/Home', component: Home },
  { path: '/Trainings', component: Trainings },
  { path: '/Gallery', component: Gallery }
]

const router = new VueRouter({
  routes
})

new Vue({
  router, 
  render: h => h(NavElement,
  {
    props:{
      navSections:[
        {text:"Home", comp:"/Home"},
        {text:"Trainings", comp:"/Trainings"},
        {text:"Gallery", comp:"/Gallery"},
      ]
    }
  }
  )
}).$mount('#app')
