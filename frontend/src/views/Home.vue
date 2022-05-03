<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png">
    <div>
      Listado de productos
     <b-container class="bv-example-row">
        <b-row>
          <b-col sm="4" v-for="product in products" :key="product.id">
            <b-card
              :title =  "product.name"
              :img-src="product.mainImage"
              img-alt="Image"
              img-top
              tag="article"
              style="width: 100%; height: 520px; margin: 10px;"
              class="mb-2"
            >
            <b-card-text>
              {{product.short}}
            </b-card-text>
            <div class="mt-2">
              <b-button variant="primary" :to="'/Product/'+product.id">Ver</b-button>
              <b-button  v-on:click="addToCart(product)">Añadir al carrito</b-button>
            </div>
            
            </b-card>
          </b-col>
        </b-row>
      </b-container>
    </div>
    <!---  PAGINATION ---->  
    <div class="overflow-auto">
      <div class="overflow-auto">
        <b-pagination
          v-model="currentPage"
          :total-rows="rows"
          :per-page="perPage"
          aria-controls="my-table"
        ></b-pagination>

        <b-table
          id="my-table"
          :items="items"
          :per-page="perPage"
          :current-page="currentPage"
          small
        ></b-table>
      </div>
    </div>
  </div>
</template>
<script>
import url from '../utils/api'
export default {
  name: 'Home',
  components: {
  },
  data(){
    return {
      products: [],
      page: 0,
      perPage: 3,
      currentPage: 1,
      items: []
    }
  },
    // para tener el nombre en la url 
  created(){
    fetch(url(`/products?_page=1&_limit=6`))
      .then(response => response.json())
      .then(data =>  {
        console.log(`data ${data}`)
        this.products = data
        })    
  },
  methods:{
      addToCart(product){
      var data = {id: product.id,
          description: product.description,
          price: product.price,
          name: product.name,
          disccount: product.disccount};
      fetch(url(`/cart`), {
        method: 'POST',
        headers:{
          'Content-type': 'application/json'
        },
        body: JSON.stringify(data)
      })
      alert("Producto Añadido")
    }
  },
  computed: {
    rows(){
      return this.items.length
    }
  }
}
 
</script>