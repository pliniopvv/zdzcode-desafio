<template>
  <v-card>
    <v-toolbar>Produtos</v-toolbar>

    <v-container>
      <v-form ref="form">
        <v-text-field
          v-model="model.nome"
          :rules="nomeRules"
          label="Produto"
          required
        ></v-text-field>
        <v-text-field
          v-model="model.descricao"
          label="Descrição"
        ></v-text-field>
        <v-select
          v-model="model.categoriaId"
          :items="listCategorias"
          :item-text="'nome'"
          :item-value="'id'"
          :rules="[(v) => !!v || 'Item is required']"
          label="Categoria"
          required
        ></v-select>

        <div class="d-flex flex-column">
          <v-btn class="mt-4" color="success" block @click="submit">
            Salvar
          </v-btn>
        </div>
        <pre>
    {{ model }}
  </pre
        >
      </v-form>
    </v-container>

    <v-footer></v-footer>

    <v-data-table
      :headers="headers"
      :items="list"
      :itens-per-page="pageSize"
      :items-length="totalItems"
      :loading="loading"
      class="elevation-1"
      @update:options="loadItems"
    ></v-data-table>
  </v-card>
</template>

<script>
export default {
  data: () => ({
    model: {
      nome: "",
      descricao: "",
    },
    nomeRules: [
      (v) => !!v || "Name é obrigatório",
      // v => (v && v.length <= 10) || 'Name must be 10 characters or less',
    ],
    listCategorias: [],

    list: [],
    headers: [
      {
        text: "Nome",
        align: "start",
        sorteable: false,
        value: "nome",
      },
      { text: "Descrição", value: "descricao" },
    ],
    pageNumber: 1,
    pageSize: 5,
    totalItems: 0,
    loading: false,
  }),
  async mounted() {
    const response = await this.$axios({
      method: "GET",
      url: "http://localhost:5291/api/produtos",
    });
    this.list = response.data.items;
    this.pageNumber = response.data.pageNumber;
    this.pageSize = response.data.pageSize;
    this.totalItems = response.data.totalItems;

    const categorias = await this.$axios({
      method: "GET",
      url: "http://localhost:5291/api/categorias",
    });
    this.listCategorias = categorias.data.items;
  },
  methods: {
    async submit() {
      try {
        const response = await this.$axios({
          method: "POST",
          url: "http://localhost:5291/api/produtos",
          data: this.model,
        });
        this.model = {};
        this.loadItems({ page: this.pageNumber, itemsPerPage: this.pageSize });
      } catch (error) {
        console.error(error);
      }
    },
    loadItems({ page, itemsPerPage }) {
      this.pageNumber = page;
      this.pageSize = itemsPerPage;
      this.loading = true;
      return this.$axios({
        method: "GET",
        url: `http://localhost:5291/api/produtos?pageNumber=${page}&pageSize=${itemsPerPage}`,
      })
        .then((resp) => {
          this.list = resp.data.items;
          this.totalItems = resp.data.totalItems;
          this.loading = false;
        })
        .catch((error) => console.error(error));
    },
  },
};
</script>

<style>
</style>