<script setup>
import { onMounted, ref } from 'vue'

const products = ref([])
const isLoading = ref(true)
const errorMessage = ref('')
const isMenuOpen = ref(false)
const selectedProduct = ref(null)
const productDialog = ref(null)

const themeClasses = {
  coral: 'bg-rose-50 text-rose-700',
  blue: 'bg-sky-50 text-sky-700',
  green: 'bg-emerald-50 text-emerald-700',
  amber: 'bg-amber-50 text-amber-700',
  violet: 'bg-violet-50 text-violet-700',
  slate: 'bg-slate-100 text-slate-700',
}

async function loadProducts() {
  isLoading.value = true
  errorMessage.value = ''

  try {
    const response = await fetch('/api/products')
    if (!response.ok) {
      throw new Error(`API returned ${response.status}`)
    }

    products.value = await response.json()
  } catch (error) {
    console.error('Kunne ikke hente forsikringsprodukter:', error)
    errorMessage.value = 'Vi kan ikke hente forsikringerne lige nu. Prøv igen om lidt.'
  } finally {
    isLoading.value = false
  }
}

function showDetails(product) {
  selectedProduct.value = product
  productDialog.value?.showModal()
}

function closeDetails() {
  productDialog.value?.close()
}

onMounted(loadProducts)
</script>

<template>
  <div class="min-h-screen overflow-hidden bg-[#fffdfb] text-[#202735]">
    <div class="bg-[#a9232b] px-4 py-2 text-center text-xs font-semibold tracking-wide text-white sm:text-sm">
      Forsikring, der passer til livet, som det er
    </div>

    <header class="relative z-10 border-b border-[#eee9e5] bg-white">
      <div class="mx-auto flex max-w-7xl items-center justify-between px-5 py-4 lg:px-8">
        <a class="flex items-center gap-3" href="#top" aria-label="Forsikring - til forsiden">
          <span class="grid size-10 place-items-center rounded-full bg-[#b92730] text-white">
            <svg viewBox="0 0 24 24" class="size-6" fill="none" aria-hidden="true">
              <path d="M12 3.2 19 6v5.2c0 4.7-2.9 8-7 9.8-4.1-1.8-7-5.1-7-9.8V6l7-2.8Z" stroke="currentColor" stroke-width="1.8" stroke-linejoin="round" />
              <path d="m8.8 12.1 2.1 2.1 4.4-4.5" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" stroke-linejoin="round" />
            </svg>
          </span>
          <span class="leading-tight">
            <span class="block text-lg font-extrabold tracking-tight text-[#202735]">forsikring</span>
            <span class="block text-[10px] font-semibold uppercase tracking-[0.2em] text-[#7b7f86]">tryghed i hverdagen</span>
          </span>
        </a>

        <nav class="hidden items-center gap-8 md:flex" aria-label="Hovednavigation">
          <a class="nav-link" href="#forsikringer">Forsikringer</a>
          <a class="nav-link" href="#fordele">Derfor os</a>
          <a class="nav-link" href="#kontakt">Kontakt</a>
        </nav>

        <a class="hidden rounded-full bg-[#b92730] px-5 py-3 text-sm font-bold text-white transition hover:bg-[#972029] md:inline-flex" href="#forsikringer">
          Find din forsikring
        </a>

        <button
          class="grid size-11 place-items-center rounded-full border border-[#e9e3df] text-[#202735] md:hidden"
          type="button"
          :aria-expanded="isMenuOpen"
          aria-label="Åbn navigation"
          @click="isMenuOpen = !isMenuOpen"
        >
          <svg v-if="!isMenuOpen" viewBox="0 0 24 24" class="size-5" fill="none" aria-hidden="true">
            <path d="M4 7h16M4 12h16M4 17h16" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" />
          </svg>
          <svg v-else viewBox="0 0 24 24" class="size-5" fill="none" aria-hidden="true">
            <path d="m6 6 12 12M18 6 6 18" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" />
          </svg>
        </button>
      </div>

      <nav v-if="isMenuOpen" class="grid gap-1 border-t border-[#eee9e5] bg-white px-5 py-3 md:hidden" aria-label="Mobilnavigation">
        <a class="mobile-nav-link" href="#forsikringer" @click="isMenuOpen = false">Forsikringer</a>
        <a class="mobile-nav-link" href="#fordele" @click="isMenuOpen = false">Derfor os</a>
        <a class="mobile-nav-link" href="#kontakt" @click="isMenuOpen = false">Kontakt</a>
      </nav>
    </header>

    <main id="top">
      <section class="relative isolate bg-[#f7f1ed]">
        <div class="absolute -right-28 -top-32 -z-10 size-[30rem] rounded-full bg-[#efe0d9] opacity-70 blur-3xl" aria-hidden="true"></div>
        <div class="mx-auto grid max-w-7xl items-center gap-10 px-5 py-14 sm:py-20 lg:grid-cols-[1.02fr_0.98fr] lg:px-8 lg:py-24">
          <div class="max-w-xl">
            <div class="mb-6 inline-flex items-center gap-2 rounded-full border border-[#e7d5cc] bg-white/70 px-4 py-2 text-xs font-bold uppercase tracking-[0.14em] text-[#9b3436]">
              <span class="size-2 rounded-full bg-[#b92730]"></span>
              Plads til det, der betyder noget
            </div>
            <h1 class="max-w-xl text-4xl font-extrabold leading-[1.08] tracking-[-0.045em] text-[#202735] sm:text-5xl lg:text-[4.25rem]">
              Godt dækket. <span class="text-[#b92730]">Helt enkelt.</span>
            </h1>
            <p class="mt-6 max-w-lg text-base leading-7 text-[#646a73] sm:text-lg sm:leading-8">
              Hverdagen er fuld af planer, små øjeblikke og store eventyr. Find forsikringer, der giver ro til at nyde det hele.
            </p>
            <div class="mt-8 flex flex-col gap-3 sm:flex-row">
              <a class="inline-flex items-center justify-center gap-2 rounded-full bg-[#b92730] px-6 py-3.5 text-sm font-bold text-white shadow-lg shadow-[#b92730]/15 transition hover:-translate-y-0.5 hover:bg-[#972029]" href="#forsikringer">
                Se vores forsikringer
                <svg viewBox="0 0 20 20" class="size-4" fill="none" aria-hidden="true">
                  <path d="M4 10h12m-5-5 5 5-5 5" stroke="currentColor" stroke-width="1.7" stroke-linecap="round" stroke-linejoin="round" />
                </svg>
              </a>
              <a class="inline-flex items-center justify-center rounded-full border border-[#d9c9c1] bg-white/60 px-6 py-3.5 text-sm font-bold text-[#3d424b] transition hover:bg-white" href="#fordele">
                Sådan hjælper vi
              </a>
            </div>
            <div class="mt-9 flex flex-wrap items-center gap-x-6 gap-y-3 text-sm font-medium text-[#686d75]">
              <span class="inline-flex items-center gap-2">
                <svg viewBox="0 0 20 20" class="size-5 text-[#b92730]" fill="none" aria-hidden="true">
                  <path d="m4 10.2 3.8 3.7L16 5.8" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
                </svg>
                Nemt at komme i gang
              </span>
              <span class="inline-flex items-center gap-2">
                <svg viewBox="0 0 20 20" class="size-5 text-[#b92730]" fill="none" aria-hidden="true">
                  <path d="M10 2.5 16 5v4.4c0 4-2.5 6.8-6 8.3-3.5-1.5-6-4.3-6-8.3V5l6-2.5Z" stroke="currentColor" stroke-width="1.6" stroke-linejoin="round" />
                </svg>
                Tryghed i hverdagen
              </span>
            </div>
          </div>

          <div class="relative mx-auto w-full max-w-[550px]">
            <div class="absolute -left-2 top-10 size-20 rounded-full bg-[#e6b29d] opacity-70 sm:-left-5 sm:size-28" aria-hidden="true"></div>
            <div class="absolute -bottom-3 right-2 size-28 rounded-full bg-[#e9c9a1] opacity-70 sm:-bottom-5 sm:right-0 sm:size-36" aria-hidden="true"></div>
            <div class="relative overflow-hidden rounded-[2rem] bg-[#edddd3] p-5 shadow-[0_24px_70px_-25px_rgba(80,48,38,0.28)] sm:rounded-[2.5rem] sm:p-8">
              <svg viewBox="0 0 560 430" class="w-full" role="img" aria-label="Illustration af et hjem med beskyttende skjold">
                <circle cx="414" cy="106" r="48" fill="#F2C98E" />
                <path d="M57 355c42-58 94-72 153-37 63 38 116 37 167-8 43-38 91-50 126-6v126H57V355Z" fill="#DFBFAF" />
                <path d="M105 207 280 74l175 133v178H105V207Z" fill="#FFFDFB" />
                <path d="M85 210 280 61l195 149-18 22-177-135L103 232l-18-22Z" fill="#B92730" />
                <path d="M220 385V263c0-25 20-45 45-45h30c25 0 45 20 45 45v122H220Z" fill="#E9C9A1" />
                <path d="M148 249h48v52h-48zM365 249h48v52h-48z" fill="#D9E7E7" />
                <path d="M172 249v52m-24-26h48m193-26v52m-24-26h48" stroke="#88A7A7" stroke-width="5" />
                <path d="M423 261c0 39-24 64-54 78-30-14-54-39-54-78v-45l54-22 54 22v45Z" fill="#B92730" />
                <path d="m346 260 16 16 31-34" stroke="white" stroke-width="10" stroke-linecap="round" stroke-linejoin="round" />
                <path d="M104 385h352" stroke="#A68170" stroke-width="6" stroke-linecap="round" />
                <path d="M78 341c8-20 23-31 43-31 20 0 35 11 43 31m275-2c7-17 20-27 37-27" stroke="#839477" stroke-width="9" stroke-linecap="round" />
                <circle cx="119" cy="300" r="15" fill="#93A58A" />
                <circle cx="453" cy="302" r="13" fill="#93A58A" />
              </svg>
              <div class="absolute bottom-7 left-7 rounded-2xl bg-white px-4 py-3 shadow-lg sm:bottom-9 sm:left-9 sm:px-5 sm:py-4">
                <p class="text-xs font-semibold text-[#858991]">Ro i maven</p>
                <p class="mt-0.5 text-sm font-extrabold text-[#202735] sm:text-base">til hverdagens eventyr</p>
              </div>
            </div>
          </div>
        </div>
      </section>

      <section id="forsikringer" class="scroll-mt-24 px-5 py-16 sm:py-20 lg:px-8">
        <div class="mx-auto max-w-7xl">
          <div class="mb-9 flex flex-col justify-between gap-5 sm:mb-12 sm:flex-row sm:items-end">
            <div class="max-w-2xl">
              <p class="mb-3 text-xs font-extrabold uppercase tracking-[0.18em] text-[#b92730]">Vælg det, du har brug for</p>
              <h2 class="text-3xl font-extrabold tracking-[-0.04em] text-[#202735] sm:text-4xl">Forsikringer til dit liv</h2>
              <p class="mt-3 text-base leading-7 text-[#737780]">Sammensæt den tryghed, der passer til dig og dem, du holder af.</p>
            </div>
            <a class="inline-flex items-center gap-2 text-sm font-bold text-[#a9232b] transition hover:gap-3" href="#kontakt">
              Få hjælp til at vælge
              <span aria-hidden="true">→</span>
            </a>
          </div>

          <div v-if="isLoading" class="grid gap-5 sm:grid-cols-2 lg:grid-cols-3" aria-label="Henter forsikringer">
            <div v-for="item in 6" :key="item" class="h-64 animate-pulse rounded-3xl border border-[#eee9e5] bg-white p-6">
              <div class="size-12 rounded-2xl bg-[#f1ece8]"></div>
              <div class="mt-7 h-5 w-2/3 rounded bg-[#f1ece8]"></div>
              <div class="mt-4 h-3 w-full rounded bg-[#f6f2ef]"></div>
              <div class="mt-2 h-3 w-4/5 rounded bg-[#f6f2ef]"></div>
            </div>
          </div>

          <div v-else-if="errorMessage" class="rounded-3xl border border-[#f0c9c8] bg-[#fff7f6] p-8 text-center" role="alert">
            <p class="font-semibold text-[#763237]">{{ errorMessage }}</p>
            <button class="mt-4 rounded-full bg-[#b92730] px-5 py-2.5 text-sm font-bold text-white hover:bg-[#972029]" type="button" @click="loadProducts">
              Prøv igen
            </button>
          </div>

          <div v-else class="grid gap-5 sm:grid-cols-2 lg:grid-cols-3">
            <article
              v-for="product in products"
              :key="product.id"
              class="group flex min-h-64 flex-col rounded-3xl border border-[#eee9e5] bg-white p-6 transition duration-200 hover:-translate-y-1 hover:border-[#e6d6ce] hover:shadow-[0_18px_40px_-24px_rgba(48,37,33,0.25)] sm:p-7"
            >
              <div class="flex items-start justify-between">
                <span class="grid size-12 place-items-center rounded-2xl" :class="themeClasses[product.theme] || 'bg-slate-100 text-slate-700'">
                  <svg v-if="product.icon === 'home'" viewBox="0 0 24 24" class="size-6" fill="none" aria-hidden="true">
                    <path d="m3 10 9-7 9 7v10a1 1 0 0 1-1 1h-5v-6H9v6H4a1 1 0 0 1-1-1V10Z" stroke="currentColor" stroke-width="1.7" stroke-linecap="round" stroke-linejoin="round" />
                  </svg>
                  <svg v-else-if="product.icon === 'car'" viewBox="0 0 24 24" class="size-6" fill="none" aria-hidden="true">
                    <path d="m5 11 1.6-4.2A2 2 0 0 1 8.5 5.5h7a2 2 0 0 1 1.9 1.3L19 11l1.5 1.5v5h-17v-5L5 11Z" stroke="currentColor" stroke-width="1.7" stroke-linejoin="round" />
                    <path d="M3.5 13h17M7 17.5h.01M17 17.5h.01M6 11l1-3h10l1 3" stroke="currentColor" stroke-width="1.7" stroke-linecap="round" />
                  </svg>
                  <svg v-else-if="product.icon === 'shield'" viewBox="0 0 24 24" class="size-6" fill="none" aria-hidden="true">
                    <path d="M12 3 19 6v5.2c0 4.7-2.9 8-7 9.8-4.1-1.8-7-5.1-7-9.8V6l7-3Z" stroke="currentColor" stroke-width="1.7" stroke-linejoin="round" />
                    <path d="m8.8 12.1 2.1 2.1 4.4-4.5" stroke="currentColor" stroke-width="1.7" stroke-linecap="round" stroke-linejoin="round" />
                  </svg>
                  <svg v-else-if="product.icon === 'heart'" viewBox="0 0 24 24" class="size-6" fill="none" aria-hidden="true">
                    <path d="M20.8 8.8c0 5.1-8.8 10.2-8.8 10.2S3.2 13.9 3.2 8.8a4.5 4.5 0 0 1 8.8-1.2 4.5 4.5 0 0 1 8.8 1.2Z" stroke="currentColor" stroke-width="1.7" stroke-linecap="round" stroke-linejoin="round" />
                  </svg>
                  <svg v-else-if="product.icon === 'plane'" viewBox="0 0 24 24" class="size-6" fill="none" aria-hidden="true">
                    <path d="m21 3-7.2 18-3.7-7.1L3 10.2 21 3Z" stroke="currentColor" stroke-width="1.7" stroke-linecap="round" stroke-linejoin="round" />
                    <path d="m10.1 13.9 4.4-4.4" stroke="currentColor" stroke-width="1.7" stroke-linecap="round" />
                  </svg>
                  <svg v-else viewBox="0 0 24 24" class="size-6" fill="none" aria-hidden="true">
                    <path d="M12 20s-7-4.3-7-10.1A3.9 3.9 0 0 1 12 7a3.9 3.9 0 0 1 7 2.9C19 15.7 12 20 12 20Z" stroke="currentColor" stroke-width="1.7" stroke-linejoin="round" />
                    <path d="M9 10.5h.01M15 10.5h.01M9.5 14c1.4 1.2 3.6 1.2 5 0" stroke="currentColor" stroke-width="1.7" stroke-linecap="round" />
                  </svg>
                </span>
                <span v-if="product.badge" class="rounded-full bg-[#fff3e7] px-3 py-1 text-[11px] font-bold text-[#9c5824]">{{ product.badge }}</span>
              </div>
              <p class="mt-6 text-xs font-bold uppercase tracking-[0.15em] text-[#92959b]">{{ product.category }}</p>
              <h3 class="mt-1 text-xl font-extrabold tracking-tight text-[#202735]">{{ product.title }}</h3>
              <p class="mt-2 flex-1 text-sm leading-6 text-[#737780]">{{ product.description }}</p>
              <button
                class="mt-5 inline-flex w-fit items-center gap-2 text-sm font-bold text-[#a9232b] transition group-hover:gap-3"
                type="button"
                @click="showDetails(product)"
              >
                Læs mere
                <span aria-hidden="true">→</span>
              </button>
            </article>
          </div>
          <p v-if="!isLoading && !errorMessage && products.length === 0" class="rounded-3xl bg-[#f7f1ed] p-8 text-center text-[#646a73]">
            Der er ingen forsikringer at vise lige nu.
          </p>
        </div>
      </section>

      <section id="fordele" class="scroll-mt-24 bg-[#f7f1ed] px-5 py-16 sm:py-20 lg:px-8">
        <div class="mx-auto grid max-w-7xl gap-10 lg:grid-cols-[0.85fr_1.15fr] lg:items-center">
          <div>
            <p class="mb-3 text-xs font-extrabold uppercase tracking-[0.18em] text-[#b92730]">Mere overskud i hverdagen</p>
            <h2 class="max-w-md text-3xl font-extrabold leading-tight tracking-[-0.04em] text-[#202735] sm:text-4xl">Det skal være rart at være godt dækket.</h2>
            <p class="mt-4 max-w-md text-base leading-7 text-[#686d75]">Vi gør det nemmere at finde rundt i forsikring, så du kan bruge tiden på alt det andet.</p>
          </div>
          <div class="grid gap-4 sm:grid-cols-3">
            <div class="rounded-3xl bg-white p-6">
              <span class="grid size-10 place-items-center rounded-xl bg-rose-50 text-[#b92730]">
                <svg viewBox="0 0 24 24" class="size-5" fill="none" aria-hidden="true"><path d="M12 3v18m6-13H9.5a3.5 3.5 0 0 0 0 7h5a3.5 3.5 0 0 1 0 7H6" stroke="currentColor" stroke-width="1.7" stroke-linecap="round" /></svg>
              </span>
              <h3 class="mt-5 font-extrabold text-[#202735]">Nemt overblik</h3>
              <p class="mt-2 text-sm leading-6 text-[#737780]">Se dine muligheder samlet og vælg i dit eget tempo.</p>
            </div>
            <div class="rounded-3xl bg-white p-6">
              <span class="grid size-10 place-items-center rounded-xl bg-sky-50 text-sky-700">
                <svg viewBox="0 0 24 24" class="size-5" fill="none" aria-hidden="true"><path d="M20 11.5a7.5 7.5 0 0 1-7.5 7.5H7l-4 2v-6.5A7.5 7.5 0 1 1 20 11.5Z" stroke="currentColor" stroke-width="1.7" stroke-linejoin="round" /></svg>
              </span>
              <h3 class="mt-5 font-extrabold text-[#202735]">Hjælp når du vil</h3>
              <p class="mt-2 text-sm leading-6 text-[#737780]">Få svar på dine spørgsmål fra rigtige mennesker.</p>
            </div>
            <div class="rounded-3xl bg-white p-6">
              <span class="grid size-10 place-items-center rounded-xl bg-emerald-50 text-emerald-700">
                <svg viewBox="0 0 24 24" class="size-5" fill="none" aria-hidden="true"><path d="m12 3 7 3v5.2c0 4.7-2.9 8-7 9.8-4.1-1.8-7-5.1-7-9.8V6l7-3Z" stroke="currentColor" stroke-width="1.7" stroke-linejoin="round" /><path d="m8.8 12.1 2.1 2.1 4.4-4.5" stroke="currentColor" stroke-width="1.7" stroke-linecap="round" stroke-linejoin="round" /></svg>
              </span>
              <h3 class="mt-5 font-extrabold text-[#202735]">Tryghed der tæller</h3>
              <p class="mt-2 text-sm leading-6 text-[#737780]">Dækning, der følger med, når livet forandrer sig.</p>
            </div>
          </div>
        </div>
      </section>

      <section id="kontakt" class="scroll-mt-24 px-5 py-16 sm:py-20 lg:px-8">
        <div class="mx-auto flex max-w-7xl flex-col items-start justify-between gap-6 rounded-[2rem] bg-[#272c37] px-7 py-9 text-white sm:px-10 sm:py-12 lg:flex-row lg:items-center">
          <div>
            <p class="text-xs font-bold uppercase tracking-[0.18em] text-[#efb8b2]">Vi er klar til at hjælpe</p>
            <h2 class="mt-3 text-3xl font-extrabold tracking-[-0.04em] sm:text-4xl">Lad os finde den rette dækning.</h2>
            <p class="mt-3 max-w-xl text-sm leading-6 text-white/70 sm:text-base">Har du spørgsmål? Tag fat i os, så hjælper vi dig godt videre.</p>
          </div>
          <a class="inline-flex shrink-0 items-center gap-2 rounded-full bg-white px-6 py-3.5 text-sm font-extrabold text-[#272c37] transition hover:bg-[#fff1ed]" href="mailto:hej@forsikring.dk">
            Skriv til os
            <span aria-hidden="true">→</span>
          </a>
        </div>
      </section>
    </main>

    <footer class="border-t border-[#eee9e5] bg-white px-5 py-7 lg:px-8">
      <div class="mx-auto flex max-w-7xl flex-col gap-3 text-xs text-[#81858c] sm:flex-row sm:items-center sm:justify-between">
        <p>© 2026 Forsikring · En demo til AI-assisteret udvikling</p>
        <p>Demoindhold – ikke et forsikringstilbud.</p>
      </div>
    </footer>

    <dialog
      ref="productDialog"
      class="m-auto w-[calc(100%-2rem)] max-w-lg rounded-3xl border-0 bg-white p-0 text-[#202735] shadow-2xl backdrop:bg-[#202735]/45"
      aria-labelledby="product-dialog-title"
      @click.self="closeDetails"
      @close="selectedProduct = null"
    >
      <div v-if="selectedProduct" class="p-7 sm:p-9">
        <div class="flex items-start justify-between gap-5">
          <div>
            <p class="text-xs font-bold uppercase tracking-[0.15em] text-[#b92730]">{{ selectedProduct.category }}</p>
            <h2 id="product-dialog-title" class="mt-2 text-2xl font-extrabold tracking-tight">{{ selectedProduct.title }}</h2>
          </div>
          <button class="grid size-10 shrink-0 place-items-center rounded-full bg-[#f5f1ee] text-[#535963] transition hover:bg-[#eee7e2]" type="button" aria-label="Luk" @click="closeDetails">
            <svg viewBox="0 0 24 24" class="size-5" fill="none" aria-hidden="true"><path d="m6 6 12 12M18 6 6 18" stroke="currentColor" stroke-width="1.8" stroke-linecap="round" /></svg>
          </button>
        </div>
        <p class="mt-5 text-base leading-7 text-[#686d75]">{{ selectedProduct.details }}</p>
        <a class="mt-7 inline-flex rounded-full bg-[#b92730] px-5 py-3 text-sm font-bold text-white transition hover:bg-[#972029]" href="#kontakt" @click="closeDetails">
          Få hjælp til at vælge
        </a>
      </div>
    </dialog>
  </div>
</template>
