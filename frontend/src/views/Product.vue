<template>
    <div style="float:left; text-align:left;margin: 50px;margin-left:10px; display:flex"> 
      <div style="width:30%">
      <router-link to="/">Volver al listado</router-link>
      <br>
              <b-button style="margin:20px" variant="primary" v-on:click="reload()" :to="'/Product/'+parseInt(product.id - 1)">Anterior</b-button>
              <b-button variant="primary" v-on:click="reload()" :to="'/Product/'+parseInt(product.id + 1)">Siguiente</b-button>
              <br>
      <img v-bind:src="product.mainImage" />
      <br>
      </div>   
      <div style="width:70%; margin-top:100px">
      <p><b>{{product.name}}</b></p>
      <p style="width: 50%">Descripción: {{product.description}}</p>
      <b-button  v-on:click="addToCart(product)">Añadir al carrito</b-button>
      </div>
    </div>  
</template>
<script>
import  url from '../utils/api'
export default{
  name: 'Product',
  components:{
  },
  data(){
    return{
      product:{}
    }
  },
  methods:{
      addToCart(product){
      var data = {id: product.id,
          description: product.description,
          price: product.price,
          name: product.name,
          disccount: product.disccount};
      fetch(url(`/cart/`), {
        method: 'POST',
        headers:{
          'Content-type': 'application/json'
        },
        body: JSON.stringify(data)
      })
      alert("Producto Añadido")
    },
    reload(){
      location.reload();
    }
  },
  computed:{},
    created(){
    fetch(url(`/products/${this.$route.params.id}`))
      .then(response => response.json())
      .then(data =>  {
        console.log(`data ${data}`)
        this.product = data
        } )    
  }}
</script>
