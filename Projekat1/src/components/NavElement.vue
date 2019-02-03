<template>
<div>
    <div id="header">
            <b-navbar toggleable="md" type="dark" >

            <b-navbar-toggle target="nav_collapse"></b-navbar-toggle>

            <router-link  :key=5 to="/Home">
                <b-navbar-brand href="#/Home" class="navElement">MEGDAN
                </b-navbar-brand>
            </router-link>


            <b-collapse is-nav id="nav_collapse">

                <b-navbar-nav>
                    <router-link v-for="(section, index) in navSections" :key="index" v-bind:to="section.comp">
                        <b-nav-item href="section.comp" class="navElement">{{section.text}}</b-nav-item>
                    </router-link>
                </b-navbar-nav>

            </b-collapse>
            </b-navbar>
    </div>

    <div id="content">
        <router-view>
        </router-view>
    </div>   
</div>
        
</template>

<script>

export default {
    name: 'NavElement',
    props: {
        navSections: Array
    }
}
</script>

<style scoped>
  @import './../Styles/NavStyle1.css';
</style>
